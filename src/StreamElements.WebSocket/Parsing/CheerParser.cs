using Newtonsoft.Json.Linq;

namespace StreamElements.WebSocket.Parsing;

public class CheerParser
{
    public static Models.Cheer.Cheer handleCheer(JToken json)
    {
        return new Models.Cheer.Cheer(json["username"].ToString(), json["providerId"].ToString(), json["displayName"].ToString(), int.Parse(json["amount"].ToString()), json["message"].ToString(), json["avatar"].ToString());
    }

    public static Models.Cheer.CheerLatest handleCheerLatest(JToken json)
    {
        return new Models.Cheer.CheerLatest(json["name"].ToString(), int.Parse(json["amount"].ToString()), json["message"].ToString());
    }

    public static int handleCheerGoal(JToken json)
    {
        return int.Parse(json["amount"].ToString());
    }

    public static int handleCheerCount(JToken json)
    {
        return int.Parse(json["count"].ToString());
    }

    public static int handleCheerTotal(JToken json)
    {
        return int.Parse(json["amount"].ToString());
    }

    public static int handleCheerSession(JToken json)
    {
        return int.Parse(json["amount"].ToString());
    }

    public static Models.Cheer.CheerSessionTopDonator handleCheerSessionTopDonator(JToken json)
    {
        return new Models.Cheer.CheerSessionTopDonator(json["name"].ToString(), int.Parse(json["amount"].ToString()));
    }

    public static Models.Cheer.CheerSessionTopDonation handleCheerSessionTopDonation(JToken json)
    {
        return new Models.Cheer.CheerSessionTopDonation(json["name"].ToString(), int.Parse(json["amount"].ToString()), json["message"].ToString());
    }

    public static int handleCheerMonth(JToken json)
    {
        return int.Parse(json["amount"].ToString());
    }

    public static int handleCheerWeek(JToken json)
    {
        return int.Parse(json["amount"].ToString());
    }
}