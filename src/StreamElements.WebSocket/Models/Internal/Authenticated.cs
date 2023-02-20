namespace StreamElements.WebSocket.Models.Internal;

public class Authenticated
{
    public string ClientId { get; }
    public string ChannelId { get; }

    public Authenticated(string clientId, string channelId)
    {
        ClientId = clientId;
        ChannelId = channelId;
    }
}