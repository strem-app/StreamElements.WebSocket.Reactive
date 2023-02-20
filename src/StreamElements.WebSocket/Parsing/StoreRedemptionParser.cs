using Newtonsoft.Json.Linq;

namespace StreamElements.WebSocket.Parsing;

public class StoreRedemptionParser
{
    public static Models.Store.StoreRedemption handleStoreRedemption(JToken json)
    {
        return new Models.Store.StoreRedemption(json["itemId"].ToString(), json["name"].ToString(), 
            json["type"].ToString(), json["item"].ToString(), json["message"]?.ToString() ?? string.Empty);
    }
}