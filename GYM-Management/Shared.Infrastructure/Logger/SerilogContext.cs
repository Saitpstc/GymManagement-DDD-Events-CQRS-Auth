namespace Shared.Infrastructure;

using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Core;

public class SerilogContext:ISerilogContext
{


    private readonly Dictionary<string, object?> Properties = new Dictionary<string, object?>();



    public void PushToLogContext(LogColumns logColumn, object? value)
    {


        Properties.Add(logColumn.ToString(), value);


    }

    public ILogEventEnricher GetEnricher()
    {
        foreach (var VARIABLE in Properties)
        {
            LogContext.PushProperty(VARIABLE.Key, VARIABLE.Value);

        }


        var dic = Properties.Where(x
                                => x.Key != LogColumns.RequestHeaders.ToString() && x.Key != LogColumns.RequestBody.ToString() &&
                                   x.Key != LogColumns.Response.ToString())
                            .ToDictionary(x => x.Key,
                                x => x.Value); // 
        var json = JsonConvert.SerializeObject(dic);

        dic = Properties.Where(x
                            => x.Key == LogColumns.RequestHeaders.ToString() || x.Key == LogColumns.RequestBody.ToString() ||
                               x.Key == LogColumns.Response.ToString())
                        .ToDictionary(x => x.Key,
                            x => x.Value);



        foreach (var kvp in dic)
        {
            json = json.Insert(json.Length - 1, $",\"{kvp.Key}\":{kvp.Value}");
        }
        LogContext.PushProperty(LogColumns.StructuredLog.ToString(), json);
        return LogContext.Clone();
    }



    public void DisposeContext()
    {
        LogContext.Reset();
        Properties.Clear();
    }
}

public class DictionaryConverter:JsonConverter
{
    private readonly List<string> _keysToExclude;

    public DictionaryConverter(List<string> keysToExclude)
    {
        _keysToExclude = keysToExclude;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var dict = (Dictionary<string, object>) value;
        writer.WriteStartObject();

        foreach (var kvp in dict)
        {
            if (!_keysToExclude.Contains(kvp.Key))
            {
                writer.WritePropertyName(kvp.Key);
                serializer.Serialize(writer, kvp.Value);
            }
        }
        writer.WriteEndObject();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(Dictionary<string, object>).IsAssignableFrom(objectType);
    }
}