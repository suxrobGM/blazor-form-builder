namespace FormBuilder.Models;

/// <summary>
/// Represents a model for enumeration dropdown items.
/// </summary>
/// <param name="Value">
/// The value of the enumeration.
/// </param>
/// <param name="Text">
/// The display text of the enumeration.
/// </param>
/// <typeparam name="TEnum">Enum type</typeparam>
public record DropDownEnumItem<TEnum>(TEnum Value, string Text);

public static class DropDownEnumItem
{
    /// <summary>
    /// Creates an array of DropDownEnumItem from the provided enum type.
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <returns>
    /// An array of DropDownEnumItem.
    /// </returns>
    public static DropDownEnumItem<TEnum>[] CreateItems<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new DropDownEnumItem<TEnum>(e, e.GetDescription()))
            .ToArray();
    }
}
