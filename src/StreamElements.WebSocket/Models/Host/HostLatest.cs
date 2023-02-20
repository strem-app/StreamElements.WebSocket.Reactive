namespace StreamElements.WebSocket.Models.Host;

public class HostLatest
{
    public string Name { get; }
    public int Amount { get; }

    public HostLatest(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}