namespace Shared.Infrastructure;

using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string? GetDescription(this Enum value)
    {

        FieldInfo field = value.GetType().GetField(value.ToString()) ;
        
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description;
    }
}