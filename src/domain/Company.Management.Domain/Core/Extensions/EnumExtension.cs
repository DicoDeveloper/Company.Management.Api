using System.ComponentModel;

namespace Company.Management.Domain.Core.Extensions;
public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        var attr = value.GetAttributes<DescriptionAttribute>();
        return attr is not null && !string.IsNullOrWhiteSpace(attr.Description) ? attr.Description : value.ToString();
    }

    private static TAttribute GetAttributes<TAttribute>(this Enum value)
            where TAttribute : class
        => value.GetType().GetMember(value.ToString()).First().GetCustomAttributes(typeof(TAttribute), false).OfType<TAttribute>().FirstOrDefault()!;
}