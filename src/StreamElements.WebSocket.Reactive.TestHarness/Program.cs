﻿using System;
using StreamElements.WebSocket.Models.Cheer;
using StreamElements.WebSocket.Models.Follower;
using StreamElements.WebSocket.Models.Host;
using StreamElements.WebSocket.Models.Internal;
using StreamElements.WebSocket.Models.Store;
using StreamElements.WebSocket.Models.Subscriber;
using StreamElements.WebSocket.Models.Tip;
using StreamElements.WebSocket.Models.Unknown;

namespace StreamElements.WebSocket.Reactive.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enter your JWT Token here and run the application
            var token = "<JWT-TOKEN>";
            
            // This uses the underlying client but can be wrapped by the reactive one
            var streamElements = new StreamElementsClient();
            streamElements.OnConnected += StreamElements_OnConnected;
            streamElements.OnAuthenticated += StreamElements_OnAuthenticated;
            streamElements.OnFollower += StreamElements_OnFollower;
            streamElements.OnSubscriber += StreamElements_OnSubscriber;
            streamElements.OnHost += StreamElements_OnHost;
            streamElements.OnTip += StreamElements_OnTip;
            streamElements.OnCheer += StreamElements_OnCheer;
            streamElements.OnStoreRedemption += StreamElements_OnStoreRedemption;
            streamElements.OnAuthenticationFailure += StreamElements_OnAuthenticationFailure;
            streamElements.OnReceivedRawMessage += StreamElements_OnReceivedRawMessage;
            streamElements.OnSent += StreamElements_OnSent;
            streamElements.OnUnknownEvent += StreamElements_OnUnknown;

            streamElements.Connect(token);

            while (true) ;
        }

        private static void StreamElements_OnSent(object sender, string e)
        {
            Console.WriteLine($"SENT: {e}");
        }

        private static void StreamElements_OnReceivedRawMessage(object sender, string e)
        {
            Console.WriteLine($"RECEIVED: {e}");
        }

        private static void StreamElements_OnAuthenticationFailure(object sender, EventArgs e)
        {
            Console.WriteLine($"Failed to login! Invalid JWT token!");
        }

        private static void StreamElements_OnCheer(object sender, Cheer e)
        {
            Console.WriteLine($"New cheer! From: {e.Username}, amount: {e.Amount}, message: {e.Message}");
        }

        private static void StreamElements_OnTip(object sender, Tip e)
        {
            Console.WriteLine($"New tip! From: {e.Username}, amount: ${e.Amount}, currency: {e.Currency}, message: {e.Message}");
        }

        private static void StreamElements_OnHost(object sender, Host e)
        {
            Console.WriteLine($"New host! Host from: {e.Username}, viewers: {e.Amount}");
        }

        private static void StreamElements_OnSubscriber(object sender, Subscriber e)
        {
            Console.WriteLine($"New subscriber! Name: {e.Username}, tier: {e.Tier}, months: {e.Amount}, gifted? {e.Gifted}, gifted by: {e.Sender}");
        }

        private static void StreamElements_OnFollower(object sender, Follower e)
        {
            Console.WriteLine($"New follower! Username: {e.Username}, userid: {e.UserId}, display name: {e.DisplayName}, avatar: {e.Avatar}");
        }

        private static void StreamElements_OnAuthenticated(object sender, Authenticated e)
        {
            Console.WriteLine($"Authenticated! Using {e.ClientId} in channel {e.ChannelId}");
        }

        private static void StreamElements_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine($"Connected!");
        }
        
        private static void StreamElements_OnStoreRedemption(object sender, StoreRedemption e)
        {
            Console.WriteLine($"Store redemption: store item {e.StoreItemName} by {e.Username} with message {e.Message}");
        }

        private static void StreamElements_OnUnknown(object sender, UnknownEventArgs e)
        {
            Console.WriteLine($"Unknown event args: {e.Type} - {e.Data}");
        }
    }
}
