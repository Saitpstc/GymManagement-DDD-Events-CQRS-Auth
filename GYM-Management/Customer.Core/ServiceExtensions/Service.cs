namespace Customer.Core.ServiceExtensions;

using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Service
{
    public static bool ValidateCountryCode(string countryCode)
    {

        var currentDirectory = Directory.GetCurrentDirectory();
        var searchPattern = @"GYM-Management\";

        var IndexOf = currentDirectory.IndexOf(searchPattern, StringComparison.Ordinal);
        if (IndexOf >= 0)
        {
            IndexOf = currentDirectory.IndexOf(searchPattern, IndexOf + 1, StringComparison.Ordinal);
        }
        string absolutePath = currentDirectory.Substring(0, IndexOf - 1 + searchPattern.Length);

        string relativePath = Path.Combine(absolutePath, "Customer.Core\\Countries.Json");
        using StreamReader reader = new StreamReader(relativePath);
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

            throw new DomainValidationException(errorMessage);
        }
    }
}