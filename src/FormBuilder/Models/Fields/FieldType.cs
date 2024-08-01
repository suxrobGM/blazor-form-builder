using System.ComponentModel;

namespace FormBuilder.Models;

/// <summary>
/// Enumeration of the input types.
/// </summary>
public enum FieldType
{
    [Description("Text")]
    Text = 1,
    
    [Description("Numeric (Int)")]
    NumericInt = 2,
    
    [Description("Numeric (Decimal)")]
    NumericDecimal = 3,
    
    [Description("Date")]
    Date = 4,
    
    [Description("Select (Dropdown)")]
    Select = 5,
}
