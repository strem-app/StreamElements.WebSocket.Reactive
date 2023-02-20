using Newtonsoft.Json.Linq;

namespace StreamElements.WebSocket.Models.Unknown;

public class UnknownEventArgs
{
    public string Type { get; }
    public JToken Data { get; }

    public UnknownEventArgs(string type, JToken data)
    {
        Type = type;
        Data = data;
    }
}