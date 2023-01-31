namespace Customer.Core.ServiceExtensions;

using System.Reflection;
using Model;
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
        var jObject = JsonConvert.DeserializeObject<JObject>(json);
        var countryList = jObject["countries"];
        var isValid = countryList.Any(x => x["code"]
            .Value<string>() == countryCode);

        return isValid;
    }
}