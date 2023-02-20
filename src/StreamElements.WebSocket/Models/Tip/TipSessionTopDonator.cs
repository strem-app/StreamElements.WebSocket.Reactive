namespace StreamElements.WebSocket.Models.Tip;

public class TipSessionTopDonator
{
    public string Name { get; }
    public double Amount { get; }

    public TipSessionTopDonator(string name, double amount)
    {
        Name = name;
        Amount = amount;
    }
}