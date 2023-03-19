namespace Shared.Infrastructure;

using System.ComponentModel;
using System.Reflection;
using Application;

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
        List<EnumResponse> list = new List<EnumResponse>();
        string[] enumNames = Enum.GetNames(typeof(T));

        foreach (string name in enumNames)
        {
            T value = (T) Enum.Parse(typeof(T), name);
            int intValue = (int) Convert.ChangeType(value, typeof(int));
            EnumResponse item = new EnumResponse { StringValue = name, IntegerValue = intValue };
            list.Add(item);
        }

        return list;
    }

    public static string CombinePermission(Enum firstValue, Enum secondValue)
    {
        return $"{firstValue.ToString()}.{secondValue.ToString()}";
    }
}