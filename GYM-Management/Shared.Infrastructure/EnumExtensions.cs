namespace Shared.Infrastructure;

using System.ComponentModel;
using System.Reflection;
using Core;

public static class EnumExtensions
{
    public static string? GetDescription(this Enum value)
    {

        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description;
    }

    public static List<EnumResponse> CreateEnumResponseList<T>() where T: Enum
    {
        var list = new List<EnumResponse>();
        var enumNames = Enum.GetNames(typeof(T));

        foreach (var name in enumNames)
        {
            T value = (T) Enum.Parse(typeof(T), name);
            var intValue = (int) Convert.ChangeType(value, typeof(int));
            EnumResponse item = new EnumResponse { StringValue = name, IntegerValue = intValue };
            list.Add(item);
        }

        return list;
    }
}