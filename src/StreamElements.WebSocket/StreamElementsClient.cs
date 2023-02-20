using System;
using System.Timers;
using Newtonsoft.Json.Linq;
using StreamElements.WebSocket.Models.Cheer;
using StreamElements.WebSocket.Models.Host;
using StreamElements.WebSocket.Models.Store;
using StreamElements.WebSocket.Models.Subscriber;
using StreamElements.WebSocket.Models.Tip;
using StreamElements.WebSocket.Models.Unknown;
using StreamElements.WebSocket.Parsing;
using WebSocket4Net;

namespace StreamElements.WebSocket;

public class StreamElementsClient : IStreamElementsClient
{
    protected readonly string StreamElementsUrl = "wss://realtime.streamelements.com/socket.io/?cluster=main&EIO=3&transport=websocket";

    public bool IsConnected { get; protected set; }
    
    protected WebSocket4Net.WebSocket client;
    protected string token;
    private Timer pingTimer;

    public event EventHandler OnConnected;
    public event EventHandler OnDisconnected;
    public event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> OnError;
    public event EventHandler<string> OnSent;
    public event EventHandler<string> OnReceivedRawMessage;

    // authentication
    public event EventHandler<Models.Internal.Authenticated> OnAuthenticated;
    public event EventHandler OnAuthenticationFailure;

    // follower
    public event EventHandler<Models.Follower.Follower> OnFollower;
    public event EventHandler<string> OnFollowerLatest;
    public event EventHandler<int> OnFollowerGoal;
    public event EventHandler<int> OnFollowerMonth;
    public event EventHandler<int> OnFollowerWeek;
    public event EventHandler<int> OnFollowerTotal;
    public event EventHandler<int> OnFollowerSession;

    // cheer
    public event EventHandler<Cheer> OnCheer;
    public event EventHandler<CheerLatest> OnCheerLatest;
    public event EventHandler<int> OnCheerGoal;
    public event EventHandler<int> OnCheerCount;
    public event EventHandler<int> OnCheerTotal;
    public event EventHandler<int> OnCheerSession;
    public event EventHandler<CheerSessionTopDonator> OnCheerSessionTopDonator;
    public event EventHandler<CheerSessionTopDonation> OnCheerSessionTopDonation;
    public event EventHandler<int> OnCheerMonth;
    public event EventHandler<int> OnCheerWeek;

    // host
    public event EventHandler<Host> OnHost;
    public event EventHandler<HostLatest> OnHostLatest;

    // tip
    public event EventHandler<Tip> OnTip;
    public event EventHandler<int> OnTipCount;
    public event EventHandler<TipLatest> OnTipLatest;
    public event EventHandler<double> OnTipSession;
    public event EventHandler<double> OnTipGoal;
    public event EventHandler<double> OnTipWeek;
    public event EventHandler<double> OnTipTotal;
    public event EventHandler<double> OnTipMonth;
    public event EventHandler<TipSessionTopDonator> OnTipSessionTopDonator;
    public event EventHandler<TipSessionTopDonation> OnTipSessionTopDonation;

    // subscriber
    public event EventHandler<Subscriber> OnSubscriber;
    public event EventHandler<SubscriberLatest> OnSubscriberLatest;
    public event EventHandler<int> OnSubscriberSession;
    public event EventHandler<int> OnSubscriberGoal;
    public event EventHandler<int> OnSubscriberMonth;
    public event EventHandler<int> OnSubscriberWeek;
    public event EventHandler<int> OnSubscriberTotal;
    public event EventHandler<int> OnSubscriberPoints;
    public event EventHandler<int> OnSubscriberResubSession;
    public event EventHandler<SubscriberResubLatest> OnSubscriberResubLatest;
    public event EventHandler<int> OnSubscriberNewSession;
    public event EventHandler<int> OnSubscriberGiftedSession;
    public event EventHandler<SubscriberNewLatest> OnSubscriberNewLatest;
    public event EventHandler<SubscriberAlltimeGifter> OnSubscriberAlltimeGifter;
    public event EventHandler<SubscriberGiftedLatest> OnSubscriberGiftedLatest;
        
    // store
    public event EventHandler<StoreRedemption> OnStoreRedemption;

    // unknowns
    public event EventHandler<UnknownEventArgs> OnUnknownEvent;

    public StreamElementsClient()
    {
        client = new WebSocket4Net.WebSocket(StreamElementsUrl);
        client.Opened += Client_Opened;
        client.Error += Client_Error;
        client.Closed += Client_Closed;
        client.MessageReceived += Client_MessageReceived;
    }

    public void Connect(string jwt)
    {
        token = jwt;
        client.Open();
    }

    public void Disconnect()
    {
        client.Close();
    }

    public void TestMessageParsing(string message)
    {
        HandleMessage(message);
    }

    public virtual void Send(string msg)
    {
        client.Send(msg);
        OnSent?.Invoke(client, msg);
    }

    protected void HandleAuthentication()
    {
        Send($"42[\"authenticate\",{{\"method\":\"jwt\",\"token\":\"{token}\"}}]");
    }

    public virtual void HandleMessage(string msg)
    {
        OnReceivedRawMessage?.Invoke(client, msg);
        // there's a number at the start of every message, figure out what it is, and remove it
        var raw = msg;
        if (raw.Contains("\""))
        {
            var number = msg.Split('"')[0].Substring(0, msg.Split('"')[0].Length - 1);
            raw = msg.Substring(number.Length);
        }
        
        if (msg.StartsWith("40"))
        {
            HandleAuthentication();
            return;
        }
        
        if (msg.StartsWith("0{\"sid\""))
        {
            HandlePingInitialization(InternalParser.handleSessionMetadata(JObject.Parse(raw)));
        }
        
        if (msg.StartsWith("42[\"authenticated\""))
        {
            OnAuthenticated?.Invoke(client, InternalParser.handleAuthenticated(JArray.Parse(raw)));
            return;
        }
        
        if (msg.StartsWith("42[\"unauthorized\""))
        {
            OnAuthenticationFailure?.Invoke(client, null);
        }
        
        if (msg.StartsWith("42[\"event\",{\"type\""))
        {
            var complexData = JArray.Parse(raw);
            handleComplexObject(complexData);
            return;
        }
        
        if (msg.StartsWith("42[\"event:update\",{\"name\""))
        {
            var simpleData = JArray.Parse(raw);
            handleSimpleUpdate(simpleData);
            return;
        }
    }

    public void handleComplexObject(JArray decoded)
    {
        var objectType = decoded[0]?.ToString() ?? string.Empty;
        if (objectType != "event") { return; }

        var eventPayload = decoded[1];
        if(eventPayload == null) { return; }
            
        var provider = eventPayload["provider"]?.ToString() ?? string.Empty;
        if (provider != "twitch") { return;}

        var eventType = eventPayload["type"]?.ToString() ?? string.Empty;
        var eventData = eventPayload["data"];

        var routed = RouteEvent(eventType, eventData);
        if (!routed)
        { OnUnknownEvent?.Invoke(client, new UnknownEventArgs(eventType, eventData)); }
    }
    
    public void handleSimpleUpdate(JArray decoded)
    {
        // only handle "event:update" types
        if (decoded[0].ToString() != "event:update") {return;}

        var eventPayload = decoded[1];
        if(eventPayload == null) { return; }
            
        var eventData = eventPayload["data"];
        var eventType = eventPayload["name"]?.ToString() ?? string.Empty;
            
        var routed = RouteEvent(eventType, eventData);
        if (!routed)
        { OnUnknownEvent?.Invoke(client, new UnknownEventArgs(eventType, eventData)); }
    }

    public virtual bool RouteEvent(string eventType, JToken eventData)
    {
        switch (eventType)
        {
            case "follow":
                OnFollower?.Invoke(client, FollowerParser.handleFollower(eventData));
                return true;
            case "cheer":
                OnCheer?.Invoke(client, CheerParser.handleCheer(eventData));
                return true;
            case "host":
                OnHost?.Invoke(client, HostParser.handleHost(eventData));
                return true;
            case "tip":
                OnTip?.Invoke(client, TipParser.handleTip(eventData));
                return true;
            case "subscriber":
                OnSubscriber?.Invoke(client, SubscriberParser.handleSubscriber(eventData));
                return true;
            case "follower-latest":
                OnFollowerLatest?.Invoke(client, FollowerParser.handleFollowerLatest(eventData));
                return true;
            case "follower-goal":
                OnFollowerGoal?.Invoke(client, FollowerParser.handleFollowerGoal(eventData));
                return true;
            case "follower-month":
                OnFollowerMonth?.Invoke(client, FollowerParser.handleFollowerMonth(eventData));
                return true;
            case "follower-week":
                OnFollowerWeek?.Invoke(client, FollowerParser.handleFollowerWeek(eventData));
                return true;
            case "follower-total":
                OnFollowerTotal?.Invoke(client, FollowerParser.handleFollowerTotal(eventData));
                return true;
            case "follower-session":
                OnFollowerSession?.Invoke(client, FollowerParser.handleFollowerSession(eventData));
                return true;
            case "cheer-latest":
                OnCheerLatest?.Invoke(client, CheerParser.handleCheerLatest(eventData));
                return true;
            case "cheer-goal":
                OnCheerGoal?.Invoke(client, CheerParser.handleCheerGoal(eventData));
                return true;
            case "cheer-count":
                OnCheerCount?.Invoke(client, CheerParser.handleCheerCount(eventData));
                return true;
            case "cheer-total":
                OnCheerTotal?.Invoke(client, CheerParser.handleCheerTotal(eventData));
                return true;
            case "cheer-session":
                OnCheerSession?.Invoke(client, CheerParser.handleCheerSession(eventData));
                return true;
            case "cheer-session-top-donator":
                OnCheerSessionTopDonator?.Invoke(client, CheerParser.handleCheerSessionTopDonator(eventData));
                return true;
            case "cheer-session-top-donation":
                OnCheerSessionTopDonation?.Invoke(client, CheerParser.handleCheerSessionTopDonation(eventData));
                return true;
            case "cheer-month":
                OnCheerMonth?.Invoke(client, CheerParser.handleCheerMonth(eventData));
                return true;
            case "cheer-week":
                OnCheerWeek?.Invoke(client, CheerParser.handleCheerWeek(eventData));
                return true;
            case "host-latest":
                OnHostLatest?.Invoke(client, HostParser.handleHostLatest(eventData));
                return true;
            case "tip-count":
                OnTipCount?.Invoke(client, TipParser.handleTipCount(eventData));
                return true;
            case "tip-latest":
                OnTipLatest?.Invoke(client, TipParser.handleTipLatest(eventData));
                return true;
            case "tip-session":
                OnTipSession?.Invoke(client, TipParser.handleTipSession(eventData));
                return true;
            case "tip-goal":
                OnTipGoal?.Invoke(client, TipParser.handleTipGoal(eventData));
                return true;
            case "tip-week":
                OnTipWeek?.Invoke(client, TipParser.handleTipWeek(eventData));
                return true;
            case "tip-total":
                OnTipTotal?.Invoke(client, TipParser.handleTipTotal(eventData));
                return true;
            case "tip-month":
                OnTipMonth?.Invoke(client, TipParser.handleTipMonth(eventData));
                return true;
            case "tip-session-top-donator":
                OnTipSessionTopDonator?.Invoke(client, TipParser.handleTipSessionTopDonator(eventData));
                return true;
            case "tip-session-top-donation":
                OnTipSessionTopDonation?.Invoke(client, TipParser.handleTipSessionTopDonation(eventData));
                return true;
            case "subscriber-latest":
                OnSubscriberLatest?.Invoke(client, SubscriberParser.handleSubscriberLatest(eventData));
                return true;
            case "subscriber-session":
                OnSubscriberSession?.Invoke(client, SubscriberParser.handleSubscriberSession(eventData));
                return true;
            case "subscriber-goal":
                OnSubscriberGoal?.Invoke(client, SubscriberParser.handleSubscriberGoal(eventData));
                return true;
            case "subscriber-month":
                OnSubscriberMonth?.Invoke(client, SubscriberParser.handleSubscriberMonth(eventData));
                return true;
            case "subscriber-week":
                OnSubscriberWeek?.Invoke(client, SubscriberParser.handleSubscriberWeek(eventData));
                return true;
            case "subscriber-total":
                OnSubscriberTotal?.Invoke(client, SubscriberParser.handleSubscriberTotal(eventData));
                return true;
            case "subscriber-points":
                OnSubscriberPoints?.Invoke(client, SubscriberParser.handleSubscriberPoints(eventData));
                return true;
            case "subscriber-resub-session":
                OnSubscriberResubSession?.Invoke(client, SubscriberParser.handleSubscriberResubSession(eventData));
                return true;
            case "subscriber-resub-latest":
                OnSubscriberResubLatest?.Invoke(client, SubscriberParser.handleSubscriberResubLatest(eventData));
                return true;
            case "subscriber-new-session":
                OnSubscriberNewSession?.Invoke(client, SubscriberParser.handleSubscriberNewSession(eventData));
                return true;
            case "subscriber-gifted-session":
                OnSubscriberGiftedSession?.Invoke(client, SubscriberParser.handleSubscriberGiftedSession(eventData));
                return true;
            case "subscriber-new-latest":
                OnSubscriberNewLatest?.Invoke(client, SubscriberParser.handleSubscriberNewLatest(eventData));
                return true;
            case "subscriber-alltime-gifter":
                OnSubscriberAlltimeGifter?.Invoke(client, SubscriberParser.handleSubscriberAlltimeGifter(eventData));
                return true;
            case "subscriber-gifted-latest":
                OnSubscriberGiftedLatest?.Invoke(client, SubscriberParser.handleSubscriberGiftedLatest(eventData));
                return true;
            case "redemption-latest":
                OnStoreRedemption?.Invoke(client, StoreRedemptionParser.handleStoreRedemption(eventData));
                return true;
            default: 
                return false;
        }
    }
    
    private void Client_Closed(object sender, EventArgs e)
    {
        IsConnected = false;
        pingTimer.Stop();
        OnDisconnected?.Invoke(sender, e);
    }

    private void Client_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
    {
        OnError?.Invoke(sender, e);
    }

    private void Client_Opened(object sender, EventArgs e)
    {
        IsConnected = true;
        OnConnected?.Invoke(sender, e);
    }

    private void HandlePingInitialization(Models.Internal.SessionMetadata md)
    {
        // start with a ping
        Send("2");
        // start ping timer
        pingTimer = new Timer(md.PingInterval);
        pingTimer.Elapsed += PingTimer_Elapsed;
        pingTimer.Start();
    }

    private void Client_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        HandleMessage(e.Message);
    }

    private void PingTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        // to remain connected, we need to send a "2" every 25 seconds
        Send("2");
    }
}