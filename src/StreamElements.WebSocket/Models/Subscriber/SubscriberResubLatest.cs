namespace StreamElements.WebSocket.Models.Subscriber;

public class SubscriberResubLatest
{
    public string Name { get; }
    public int Amount { get; }
    public string Message { get; }

    public SubscriberResubLatest(string name, int amount, string message)
    {
        Name = name;
        Amount = amount;
        Message = message;
    }
}