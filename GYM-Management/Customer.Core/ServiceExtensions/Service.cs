namespace Customer.Core.ServiceExtensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Service
{
    public static bool ValidateCountryCode(string countryCode)
    {
        var path = Directory.GetParent(Directory.GetCurrentDirectory())
                            .Parent.Parent.Parent.FullName + "/Customer.Core/Countries.json";
        using StreamReader reader = new StreamReader(path);
        var json = reader.ReadToEnd();
        JObject? jObject = JsonConvert.DeserializeObject<JObject>(json);
        JToken? countryList = jObject["countries"];
        var isValid = countryList.Any(x => x["code"]
            .Value<string>() == countryCode);

        return isValid;
    }
}