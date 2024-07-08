using System.ComponentModel;

namespace Company.Management.Domain.Core.Extensions;
public static class StringExtension
{
    public static T StringToEnum<T>(this string str, int defaultValue = 0) where T : Enum
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return (T)Enum.GetValues(typeof(T)).GetValue(defaultValue)!;
        }

        var type = typeof(T);
        var fields = type.GetFields();
        str = str.Trim();

        foreach (var field in fields)
        {
            var descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                             .FirstOrDefault() as DescriptionAttribute;

            if (descriptionAttribute is not null && descriptionAttribute.Description.Equals(str, StringComparison.OrdinalIgnoreCase))
            {
                return (T)field.GetValue(null)!;
            }
        }

        try
        {
            return (T)Enum.Parse(type, str, true);
        }
        catch
        {
            return (T)Enum.GetValues(typeof(T)).GetValue(defaultValue)!;
        }
    }
}