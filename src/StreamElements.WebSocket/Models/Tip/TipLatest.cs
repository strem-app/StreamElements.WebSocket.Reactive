namespace StreamElements.WebSocket.Models.Tip;

public class TipLatest
{
    public string Name { get; }
    public double Amount { get; }
    public string Message { get; }

    public TipLatest(string name, double amount, string message)
    {
        Name = name;
        Amount = amount;
        Message = message;
    }
}