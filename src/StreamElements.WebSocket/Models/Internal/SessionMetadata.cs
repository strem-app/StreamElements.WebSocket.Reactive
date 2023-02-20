namespace StreamElements.WebSocket.Models.Internal;

public class SessionMetadata
{
    public string SID { get; }
    public int PingInterval { get; }
    public int PingTimeout { get; }

    public SessionMetadata(string sid, int pingInterval, int pingTimeout)
    {
        SID = sid;
        PingInterval = pingInterval;
        PingTimeout = pingTimeout;
    }
}