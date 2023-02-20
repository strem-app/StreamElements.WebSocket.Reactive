﻿namespace StreamElements.WebSocket.Models.Cheer;

public class Cheer
{
    public string Username { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public int Amount { get; }
    public string Message { get; }
    public string Avatar { get; }

    public Cheer(string username, string userId, string displayName, int amount, string message, string avatar)
    {
        Username = username;
        UserId = userId;
        Amount = amount;
        Message = message;
        Avatar = avatar;
    }
}