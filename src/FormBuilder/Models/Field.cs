using System.Text.Json.Serialization;
using FormBuilder.Converters;
using FormBuilder.Utils;

namespace FormBuilder.Models;

/// <summary>
/// Represents a model for the form field.
/// </summary>
[JsonConverter(typeof(FieldJsonConverter))]
public abstract class Field
{
    private string? _name;

    /// <summary>
    /// Field name. If not provided, it will be generated.
    /// </summary>
    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(_name))
            {
                _name = Generator.GenerateShortId($"{Type}_".ToLower());
            }

            return _name;
        }
        set => _name = value;
    }
    
    /// <summary>
    /// Field label.
    /// </summary>
    public string? Label { get; set; }
    
    /// <summary>
    /// Input placeholder.
    /// </summary>
    public string? Placeholder { get; set; }
    
    /// <summary>
    /// Field type such as TextField, NumericIntField, NumericDecimalField, SelectField, DateField.
    /// </summary>
    public abstract FieldType Type { get; }
    
    /// <summary>
    /// Whether the field is read-only.
    /// </summary>
    public bool ReadOnly { get; set; }
    
    /// <summary>
    /// Whether the field is disabled.
    /// </summary>
    public bool Disabled { get; set; }
    
    /// <summary>
    /// The hint text to be displayed below the field.
    /// </summary>
    public string? Hint { get; set; }
    
    /// <summary>
    /// List of validators to be applied to the field.
    /// </summary>
    public List<Validator> Validators { get; set; } = [];
}

/// <summary>
/// Generic version of the field model with a value of type T.
/// </summary>
/// <typeparam name="T">The type of the field value.</typeparam>
public abstract class Field<T> : Field
{
    public T? Value { get; set; }
}
