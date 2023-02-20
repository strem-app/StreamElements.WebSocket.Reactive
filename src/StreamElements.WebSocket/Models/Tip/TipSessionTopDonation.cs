namespace StreamElements.WebSocket.Models.Tip;

public class TipSessionTopDonation
{
    public string Name { get; }
    public double Amount { get; }

    public TipSessionTopDonation(string name, double amount)
    {
        Name = name;
        Amount = amount;
    }
}