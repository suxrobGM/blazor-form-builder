namespace FormBuilder.Models;

/// <summary>
/// Parameters for the FieldTypeChanged event.
/// </summary>
public class FieldTypeChangedArgs : EventArgs
{
    /// <summary>
    /// The field that has changed.
    /// </summary>
    public required Field Field { get; set; }
    
    /// <summary>
    /// The new type of the field.
    /// </summary>
    public required FieldType NewType { get; set; }
}
