using System.Text.Json.Serialization;
using FormBuilder.Converters;

namespace FormBuilder.Models;

/// <summary>
/// Represents a validator that can be applied to a form field.
/// </summary>
[JsonConverter(typeof(ValidatorJsonConverter))]
public abstract class Validator : ObservableBase
{
    /// <summary>
    /// The type of validator.
    /// </summary>
    public abstract ValidatorType Type { get; }
    
    /// <summary>
    /// The error text to be displayed when the validation fails.
    /// </summary>
    public abstract string Text { get; set; }
    
    /// <summary>
    /// Whether the validation message should be shown as a popup instead of inline.
    /// </summary>
    private bool _showAsPopup;
    public bool ShowAsPopup
    {
        get => _showAsPopup;
        set => SetField(ref _showAsPopup, value);
    }
}
