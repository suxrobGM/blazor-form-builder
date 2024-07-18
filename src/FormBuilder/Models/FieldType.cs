using System.ComponentModel;

namespace FormBuilder.Models;

/// <summary>
/// Enumeration of the input types.
/// </summary>
public enum FieldType
{
    [Description("Text")]
    Text,
    
    [Description("Numeric (Int)")]
    NumericInt,
    
    [Description("Numeric (Double)")]
    NumericDouble,
    
    [Description("Date")]
    Date,
    
    [Description("Select (Dropdown)")]
    Select,
}
