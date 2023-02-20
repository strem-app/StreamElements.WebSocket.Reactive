using System;
using System.Reactive;
using System.Reactive.Linq;
using StreamElements.WebSocket.Models.Cheer;
using StreamElements.WebSocket.Models.Follower;
using StreamElements.WebSocket.Models.Host;
using StreamElements.WebSocket.Models.Internal;
using StreamElements.WebSocket.Models.Subscriber;
using StreamElements.WebSocket.Models.Tip;
using StreamElements.WebSocket.Models.Unknown;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace StreamElements.WebSocket.Reactive;

public class ObservableStreamElementsClient : IObservableStreamElementsClient
{
    public IStreamElementsClient WebSocketClient { get; }
    
    public IObservable<Unit> OnConnected { get; private set; }
    public IObservable<Unit> OnDisconnected { get; private set; }
    public IObservable<Authenticated> OnAuthenticated { get; private set; }
    public IObservable<ErrorEventArgs> OnError { get; private set; }
    public IObservable<Unit> OnAuthenticationFailure { get; private set; }
    public IObservable<Follower> OnFollower { get; private set; }
    public IObservable<string> OnFollowerLatest { get; private set; }
    public IObservable<int> OnFollowerGoal { get; private set; }
    public IObservable<int> OnFollowerMonth { get; private set; }
    public IObservable<int> OnFollowerWeek { get; private set; }
    public IObservable<int> OnFollowerTotal { get; private set; }
    public IObservable<int> OnFollowerSession { get; private set; }
    public IObservable<Cheer> OnCheer { get; private set; }
    public IObservable<CheerLatest> OnCheerLatest { get; private set; }
    public IObservable<int> OnCheerGoal { get; private set; }
    public IObservable<int> OnCheerCount { get; private set; }
    public IObservable<int> OnCheerTotal { get; private set; }
    public IObservable<int> OnCheerSession { get; private set; }
    public IObservable<CheerSessionTopDonator> OnCheerSessionTopDonator { get; private set; }
    public IObservable<CheerSessionTopDonation> OnCheerSessionTopDonation { get; private set; }
    public IObservable<int> OnCheerMonth { get; private set; }
    public IObservable<int> OnCheerWeek { get; private set; }
    public IObservable<Host> OnHost { get; private set; }
    public IObservable<HostLatest> OnHostLatest { get; private set; }
    public IObservable<Tip> OnTip { get; private set; }
    public IObservable<int> OnTipCount { get; private set; }
    public IObservable<TipLatest> OnTipLatest { get; private set; }
    public IObservable<double> OnTipSession { get; private set; }
    public IObservable<double> OnTipGoal { get; private set; }
    public IObservable<double> OnTipWeek { get; private set; }
    public IObservable<double> OnTipTotal { get; private set; }
    public IObservable<double> OnTipMonth { get; private set; }
    public IObservable<TipSessionTopDonator> OnTipSessionTopDonator { get; private set; }
    public IObservable<TipSessionTopDonation> OnTipSessionTopDonation { get; private set; }
    public IObservable<Subscriber> OnSubscriber { get; private set; }
    public IObservable<SubscriberLatest> OnSubscriberLatest { get; private set; }
    public IObservable<int> OnSubscriberSession { get; private set; }
    public IObservable<int> OnSubscriberGoal { get; private set; }
    public IObservable<int> OnSubscriberMonth { get; private set; }
    public IObservable<int> OnSubscriberWeek { get; private set; }
    public IObservable<int> OnSubscriberTotal { get; private set; }
    public IObservable<int> OnSubscriberPoints { get; private set; }
    public IObservable<int> OnSubscriberResubSession { get; private set; }
    public IObservable<SubscriberResubLatest> OnSubscriberResubLatest { get; private set; }
    public IObservable<int> OnSubscriberNewSession { get; private set; }
    public IObservable<int> OnSubscriberGiftedSession { get; private set; }
    public IObservable<SubscriberNewLatest> OnSubscriberNewLatest { get; private set; }
    public IObservable<SubscriberAlltimeGifter> OnSubscriberAlltimeGifter { get; private set; }
    public IObservable<SubscriberGiftedLatest> OnSubscriberGiftedLatest { get; private set; }
    
    public IObservable<UnknownEventArgs> OnUnknownEvent { get; private set; }
    
    public ObservableStreamElementsClient(IStreamElementsClient client)
    {
        WebSocketClient = client;
        SetupObservables();
    }

    public void SetupObservables()
    {
        OnConnected = Observable.FromEventPattern(
                e => WebSocketClient.OnConnected += e,
                e => WebSocketClient.OnConnected -= e)
            .Select(x => Unit.Default);

        OnDisconnected = Observable.FromEventPattern(
                e => WebSocketClient.OnDisconnected += e,
                e => WebSocketClient.OnDisconnected -= e)
            .Select(x => Unit.Default);
        
        OnCheer = Observable.FromEventPattern<Cheer>(
                e => WebSocketClient.OnCheer += e,
                e => WebSocketClient.OnCheer -= e)
            .Select(x => x.EventArgs);
        
        OnAuthenticated = Observable.FromEventPattern<Authenticated>(
                e => WebSocketClient.OnAuthenticated += e,
                e => WebSocketClient.OnAuthenticated -= e)
            .Select(x => x.EventArgs);
        
        OnError = Observable.FromEventPattern<ErrorEventArgs>(
                e => WebSocketClient.OnError += e,
                e => WebSocketClient.OnError -= e)
            .Select(x => x.EventArgs);
        
        OnFollower = Observable.FromEventPattern<Follower>(
                e => WebSocketClient.OnFollower += e,
                e => WebSocketClient.OnFollower -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerWeek = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnFollowerWeek += e,
                e => WebSocketClient.OnFollowerWeek -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerGoal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnFollowerGoal += e,
                e => WebSocketClient.OnFollowerGoal -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerMonth = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnFollowerMonth += e,
                e => WebSocketClient.OnFollowerMonth -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerLatest = Observable.FromEventPattern<string>(
                e => WebSocketClient.OnFollowerLatest += e,
                e => WebSocketClient.OnFollowerLatest -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnFollowerSession += e,
                e => WebSocketClient.OnFollowerSession -= e)
            .Select(x => x.EventArgs);
        
        OnFollowerTotal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnFollowerTotal += e,
                e => WebSocketClient.OnFollowerTotal -= e)
            .Select(x => x.EventArgs);
        
        OnHost = Observable.FromEventPattern<Host>(
                e => WebSocketClient.OnHost += e,
                e => WebSocketClient.OnHost -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriber = Observable.FromEventPattern<Subscriber>(
                e => WebSocketClient.OnSubscriber += e,
                e => WebSocketClient.OnSubscriber -= e)
            .Select(x => x.EventArgs);
        
        OnTip = Observable.FromEventPattern<Tip>(
                e => WebSocketClient.OnTip += e,
                e => WebSocketClient.OnTip -= e)
            .Select(x => x.EventArgs);
        
        OnCheerCount = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerCount += e,
                e => WebSocketClient.OnCheerCount -= e)
            .Select(x => x.EventArgs);
                
        OnCheerGoal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerGoal += e,
                e => WebSocketClient.OnCheerGoal -= e)
            .Select(x => x.EventArgs);
        
        OnCheerSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerSession += e,
                e => WebSocketClient.OnCheerSession -= e)
            .Select(x => x.EventArgs);
        
        OnCheerMonth = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerMonth += e,
                e => WebSocketClient.OnCheerMonth -= e)
            .Select(x => x.EventArgs);
        
        OnCheerTotal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerTotal += e,
                e => WebSocketClient.OnCheerTotal -= e)
            .Select(x => x.EventArgs);
        
        OnCheerWeek = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnCheerWeek += e,
                e => WebSocketClient.OnCheerWeek -= e)
            .Select(x => x.EventArgs);
        
        OnCheerLatest = Observable.FromEventPattern<CheerLatest>(
                e => WebSocketClient.OnCheerLatest += e,
                e => WebSocketClient.OnCheerLatest -= e)
            .Select(x => x.EventArgs);
        
        OnCheerSessionTopDonation = Observable.FromEventPattern<CheerSessionTopDonation>(
                e => WebSocketClient.OnCheerSessionTopDonation += e,
                e => WebSocketClient.OnCheerSessionTopDonation -= e)
            .Select(x => x.EventArgs);
                
        OnCheerSessionTopDonator = Observable.FromEventPattern<CheerSessionTopDonator>(
                e => WebSocketClient.OnCheerSessionTopDonator += e,
                e => WebSocketClient.OnCheerSessionTopDonator -= e)
            .Select(x => x.EventArgs);
        
        OnHostLatest = Observable.FromEventPattern<HostLatest>(
                e => WebSocketClient.OnHostLatest += e,
                e => WebSocketClient.OnHostLatest -= e)
            .Select(x => x.EventArgs);

        OnAuthenticationFailure = Observable.FromEventPattern(
                e => WebSocketClient.OnAuthenticationFailure += e,
                e => WebSocketClient.OnAuthenticationFailure -= e)
            .Select(x => Unit.Default);
        
        OnTipCount = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnTipCount += e,
                e => WebSocketClient.OnTipCount -= e)
            .Select(x => x.EventArgs);
        
        OnTipLatest = Observable.FromEventPattern<TipLatest>(
                e => WebSocketClient.OnTipLatest += e,
                e => WebSocketClient.OnTipLatest -= e)
            .Select(x => x.EventArgs);
        
        OnTipSession = Observable.FromEventPattern<double>(
                e => WebSocketClient.OnTipSession += e,
                e => WebSocketClient.OnTipSession -= e)
            .Select(x => x.EventArgs);
        
        OnTipGoal = Observable.FromEventPattern<double>(
                e => WebSocketClient.OnTipGoal += e,
                e => WebSocketClient.OnTipGoal -= e)
            .Select(x => x.EventArgs);
        
        OnTipWeek = Observable.FromEventPattern<double>(
                e => WebSocketClient.OnTipWeek += e,
                e => WebSocketClient.OnTipWeek -= e)
            .Select(x => x.EventArgs);
        
        OnTipTotal = Observable.FromEventPattern<double>(
                e => WebSocketClient.OnTipTotal += e,
                e => WebSocketClient.OnTipTotal -= e)
            .Select(x => x.EventArgs);
        
        OnTipMonth = Observable.FromEventPattern<double>(
                e => WebSocketClient.OnTipMonth += e,
                e => WebSocketClient.OnTipMonth -= e)
            .Select(x => x.EventArgs);
        
        OnTipSessionTopDonator = Observable.FromEventPattern<TipSessionTopDonator>(
                e => WebSocketClient.OnTipSessionTopDonator += e,
                e => WebSocketClient.OnTipSessionTopDonator -= e)
            .Select(x => x.EventArgs);
        
        OnTipSessionTopDonation = Observable.FromEventPattern<TipSessionTopDonation>(
                e => WebSocketClient.OnTipSessionTopDonation += e,
                e => WebSocketClient.OnTipSessionTopDonation -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberLatest = Observable.FromEventPattern<SubscriberLatest>(
                e => WebSocketClient.OnSubscriberLatest += e,
                e => WebSocketClient.OnSubscriberLatest -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberSession += e,
                e => WebSocketClient.OnSubscriberSession -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberGoal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberGoal += e,
                e => WebSocketClient.OnSubscriberGoal -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberMonth = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberMonth += e,
                e => WebSocketClient.OnSubscriberMonth -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberWeek = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberWeek += e,
                e => WebSocketClient.OnSubscriberWeek -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberTotal = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberTotal += e,
                e => WebSocketClient.OnSubscriberTotal -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberPoints = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberPoints += e,
                e => WebSocketClient.OnSubscriberPoints -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberResubSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberResubSession += e,
                e => WebSocketClient.OnSubscriberResubSession -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberResubLatest = Observable.FromEventPattern<SubscriberResubLatest>(
                e => WebSocketClient.OnSubscriberResubLatest += e,
                e => WebSocketClient.OnSubscriberResubLatest -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberNewSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberNewSession += e,
                e => WebSocketClient.OnSubscriberNewSession -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberGiftedSession = Observable.FromEventPattern<int>(
                e => WebSocketClient.OnSubscriberGiftedSession += e,
                e => WebSocketClient.OnSubscriberGiftedSession -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberNewLatest = Observable.FromEventPattern<SubscriberNewLatest>(
                e => WebSocketClient.OnSubscriberNewLatest += e,
                e => WebSocketClient.OnSubscriberNewLatest -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberAlltimeGifter = Observable.FromEventPattern<SubscriberAlltimeGifter>(
                e => WebSocketClient.OnSubscriberAlltimeGifter += e,
                e => WebSocketClient.OnSubscriberAlltimeGifter -= e)
            .Select(x => x.EventArgs);
        
        OnSubscriberGiftedLatest = Observable.FromEventPattern<SubscriberGiftedLatest>(
                e => WebSocketClient.OnSubscriberGiftedLatest += e,
                e => WebSocketClient.OnSubscriberGiftedLatest -= e)
            .Select(x => x.EventArgs);
        
        OnUnknownEvent = Observable.FromEventPattern<UnknownEventArgs>(
                e => WebSocketClient.OnUnknownEvent += e,
                e => WebSocketClient.OnUnknownEvent -= e)
            .Select(x => x.EventArgs);


    }
}