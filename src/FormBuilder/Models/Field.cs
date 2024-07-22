using FormBuilder.Utils;

namespace FormBuilder.Models;

/// <summary>
/// Represents a model for the form field.
/// </summary>
public abstract class Field
{
    protected Field()
    {
        if (string.IsNullOrEmpty(Name))
        {
            Name = Generator.GenerateShortId($"{Type}_".ToLower());
        }
    }
    
    /// <summary>
    /// Field name. If not provided, it will be generated.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Field label.
    /// </summary>
    public string? Label { get; set; }
    
    /// <summary>
    /// Input placeholder.
    /// </summary>
    public string? Placeholder { get; set; }
    
    /// <summary>
    /// Field type such as TextField, NumericIntField, NumericDoubleField, SelectField, DateField.
    /// </summary>
    public FieldType Type { get; set; }
    
    /// <summary>
    /// Determines if the field is required to be filled.
    /// </summary>
    public bool Required { get; set; }
    
    /// <summary>
    /// Whether the field is read-only.
    /// </summary>
    public bool ReadOnly { get; set; }
    
    /// <summary>
    /// Whether the field is disabled.
    /// </summary>
    public bool Disabled { get; set; }
}

/// <summary>
/// Generic version of the field model with a value of type T.
/// </summary>
/// <typeparam name="T">The type of the field value.</typeparam>
public abstract class Field<T> : Field
{
    public T? Value { get; set; }
}
