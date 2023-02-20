﻿using System;
using System.Reactive;
using StreamElements.WebSocket.Models.Cheer;
using StreamElements.WebSocket.Models.Follower;
using StreamElements.WebSocket.Models.Host;
using StreamElements.WebSocket.Models.Internal;
using StreamElements.WebSocket.Models.Subscriber;
using StreamElements.WebSocket.Models.Tip;
using StreamElements.WebSocket.Models.Unknown;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace StreamElements.WebSocket.Reactive;

public interface IObservableStreamElementsClient
{
    IStreamElementsClient WebSocketClient { get; }
    
    IObservable<Unit> OnConnected { get; }
    IObservable<Unit> OnDisconnected { get; }
    IObservable<Authenticated> OnAuthenticated { get; }
    IObservable<ErrorEventArgs> OnError { get; }
    IObservable<Unit> OnAuthenticationFailure { get; }
    IObservable<Follower> OnFollower { get; }
    IObservable<string> OnFollowerLatest { get; }
    IObservable<int> OnFollowerGoal { get; }
    IObservable<int> OnFollowerMonth { get; }
    IObservable<int> OnFollowerWeek { get; }
    IObservable<int> OnFollowerTotal { get; }
    IObservable<int> OnFollowerSession { get; }
    IObservable<Cheer> OnCheer { get; }
    IObservable<CheerLatest> OnCheerLatest { get; }
    IObservable<int> OnCheerGoal { get; }
    IObservable<int> OnCheerCount { get; }
    IObservable<int> OnCheerTotal { get; }
    IObservable<int> OnCheerSession { get; }
    IObservable<CheerSessionTopDonator> OnCheerSessionTopDonator { get; }
    IObservable<CheerSessionTopDonation> OnCheerSessionTopDonation { get; }
    IObservable<int> OnCheerMonth { get; }
    IObservable<int> OnCheerWeek { get; }
    IObservable<Host> OnHost { get; }
    IObservable<HostLatest> OnHostLatest { get; }
    IObservable<Tip> OnTip { get; }
    IObservable<int> OnTipCount { get; }
    IObservable<TipLatest> OnTipLatest { get; }
    IObservable<double> OnTipSession { get; }
    IObservable<double> OnTipGoal { get; }
    IObservable<double> OnTipWeek { get; }
    IObservable<double> OnTipTotal { get; }
    IObservable<double> OnTipMonth { get; }
    IObservable<TipSessionTopDonator> OnTipSessionTopDonator { get; }
    IObservable<TipSessionTopDonation> OnTipSessionTopDonation { get; }
    IObservable<Subscriber> OnSubscriber { get; }
    IObservable<SubscriberLatest> OnSubscriberLatest { get; }
    IObservable<int> OnSubscriberSession { get; }
    IObservable<int> OnSubscriberGoal { get; }
    IObservable<int> OnSubscriberMonth { get; }
    IObservable<int> OnSubscriberWeek { get; }
    IObservable<int> OnSubscriberTotal { get; }
    IObservable<int> OnSubscriberPoints { get; }
    IObservable<int> OnSubscriberResubSession { get; }
    IObservable<SubscriberResubLatest> OnSubscriberResubLatest { get; }
    IObservable<int> OnSubscriberNewSession { get; }
    IObservable<int> OnSubscriberGiftedSession { get; }
    IObservable<SubscriberNewLatest> OnSubscriberNewLatest { get; }
    IObservable<SubscriberAlltimeGifter> OnSubscriberAlltimeGifter { get; }
    IObservable<SubscriberGiftedLatest> OnSubscriberGiftedLatest { get; }
    
    IObservable<UnknownEventArgs> OnUnknownEvent { get; }
}