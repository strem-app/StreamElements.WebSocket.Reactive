namespace StreamElements.WebSocket.Models.Subscriber;

public class SubscriberAlltimeGifter
{
    public string Name { get; }
    public int Amount { get; }

    public SubscriberAlltimeGifter(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}