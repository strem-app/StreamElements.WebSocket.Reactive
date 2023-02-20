namespace StreamElements.WebSocket.Models.Follower;

public class Follower
{
    public string Username { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public string Avatar { get; }

    public Follower(string username, string userId, string displayName, string avatar)
    {
        Username = username;
        UserId = userId;
        DisplayName = displayName;
        Avatar = avatar;
    }
}