namespace StreamElements.WebSocket.Models.Cheer;

public class CheerSessionTopDonator
{
    public string Name { get; }
    public int Amount { get; }

    public CheerSessionTopDonator(string name, int amount)
    {
        Name = name;
        Amount = amount;
    }
}