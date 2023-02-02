namespace Customer.Core.ServiceExtensions;

using Customer.Exceptions;
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

    public static void CheckIfStringValid(string input, string? errorMessage = null)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (errorMessage == null)
            {
                throw new DomainValidationException("Input Is Not Valid");
            }
            else
            {
                throw new DomainValidationException(errorMessage);
            }
        }
    }
}