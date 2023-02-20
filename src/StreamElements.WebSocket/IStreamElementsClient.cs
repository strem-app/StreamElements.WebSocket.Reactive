using System;
using StreamElements.WebSocket.Models.Cheer;
using StreamElements.WebSocket.Models.Follower;
using StreamElements.WebSocket.Models.Host;
using StreamElements.WebSocket.Models.Internal;
using StreamElements.WebSocket.Models.Store;
using StreamElements.WebSocket.Models.Subscriber;
using StreamElements.WebSocket.Models.Tip;
using StreamElements.WebSocket.Models.Unknown;

namespace StreamElements.WebSocket;

public interface IStreamElementsClient
{
    bool IsConnected { get; }
    
    event EventHandler OnConnected;
    event EventHandler OnDisconnected;
    event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> OnError;
    event EventHandler<string> OnSent;
    event EventHandler<string> OnReceivedRawMessage;
    event EventHandler<Authenticated> OnAuthenticated;
    event EventHandler OnAuthenticationFailure;
    event EventHandler<Follower> OnFollower;
    event EventHandler<string> OnFollowerLatest;
    event EventHandler<int> OnFollowerGoal;
    event EventHandler<int> OnFollowerMonth;
    event EventHandler<int> OnFollowerWeek;
    event EventHandler<int> OnFollowerTotal;
    event EventHandler<int> OnFollowerSession;
    event EventHandler<Cheer> OnCheer;
    event EventHandler<CheerLatest> OnCheerLatest;
    event EventHandler<int> OnCheerGoal;
    event EventHandler<int> OnCheerCount;
    event EventHandler<int> OnCheerTotal;
    event EventHandler<int> OnCheerSession;
    event EventHandler<CheerSessionTopDonator> OnCheerSessionTopDonator;
    event EventHandler<CheerSessionTopDonation> OnCheerSessionTopDonation;
    event EventHandler<int> OnCheerMonth;
    event EventHandler<int> OnCheerWeek;
    event EventHandler<Host> OnHost;
    event EventHandler<HostLatest> OnHostLatest;
    event EventHandler<Tip> OnTip;
    event EventHandler<int> OnTipCount;
    event EventHandler<TipLatest> OnTipLatest;
    event EventHandler<double> OnTipSession;
    event EventHandler<double> OnTipGoal;
    event EventHandler<double> OnTipWeek;
    event EventHandler<double> OnTipTotal;
    event EventHandler<double> OnTipMonth;
    event EventHandler<TipSessionTopDonator> OnTipSessionTopDonator;
    event EventHandler<TipSessionTopDonation> OnTipSessionTopDonation;
    event EventHandler<Subscriber> OnSubscriber;
    event EventHandler<SubscriberLatest> OnSubscriberLatest;
    event EventHandler<int> OnSubscriberSession;
    event EventHandler<int> OnSubscriberGoal;
    event EventHandler<int> OnSubscriberMonth;
    event EventHandler<int> OnSubscriberWeek;
    event EventHandler<int> OnSubscriberTotal;
    event EventHandler<int> OnSubscriberPoints;
    event EventHandler<int> OnSubscriberResubSession;
    event EventHandler<SubscriberResubLatest> OnSubscriberResubLatest;
    event EventHandler<int> OnSubscriberNewSession;
    event EventHandler<int> OnSubscriberGiftedSession;
    event EventHandler<SubscriberNewLatest> OnSubscriberNewLatest;
    event EventHandler<SubscriberAlltimeGifter> OnSubscriberAlltimeGifter;
    event EventHandler<SubscriberGiftedLatest> OnSubscriberGiftedLatest;
    event EventHandler<StoreRedemption> OnStoreRedemption;
    event EventHandler<UnknownEventArgs> OnUnknownEvent;
    
    void Connect(string jwt);
    void Disconnect();
}