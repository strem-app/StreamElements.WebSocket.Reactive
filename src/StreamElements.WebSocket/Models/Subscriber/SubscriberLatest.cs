namespace StreamElements.WebSocket.Models.Subscriber;

public class SubscriberLatest
{
    public string Name { get; }
    public int Amount { get; }
    public string Tier { get; }
    public string Message { get; }
    public bool Gifted { get; }
    public string Sender { get; }

    public SubscriberLatest(string name, int amount, string tier, string message, bool gifted, string sender)
    {
        Name = name;
        Amount = amount;
        Tier = tier;
        Message = message;
    }
}