using System.ComponentModel;

namespace System;

/// <summary>
/// Provides extension methods for the Enum type.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description of the enum value marked with the DescriptionAttribute.
    /// </summary>
    /// <param name="enumValue">
    /// The enum value.
    /// </param>
    /// <returns>
    /// The description of the enum value marked with the DescriptionAttribute or the enum name if the attribute is not found.
    /// </returns>
    public static string GetDescription(this Enum enumValue)
    {
        var type = enumValue.GetType();
        var fieldInfo = type.GetField(enumValue.ToString());

        if (fieldInfo is null)
        {
            return enumValue.ToString();
        }

        var descriptionAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

        // Return the description, if it exists; otherwise, return the enum name
        return descriptionAttribute?.Description ?? enumValue.ToString();
    }
}
