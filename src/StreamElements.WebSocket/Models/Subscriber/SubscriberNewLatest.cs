namespace StreamElements.WebSocket.Models.Subscriber;

public class SubscriberNewLatest
{
    public string Name { get; }
    public int Amount { get; }
    public string Message { get; }

    public SubscriberNewLatest(string name, int amount, string message)
    {
        Name = name;
        Amount = amount;
        Message = message;
    }
}