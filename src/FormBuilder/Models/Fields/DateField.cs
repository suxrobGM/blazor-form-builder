namespace FormBuilder.Models;

/// <summary>
/// Represents a date field.
/// The value is of type DateTime.
/// </summary>
public class DateField : Field<DateTime?>
{
    public override FieldType Type => FieldType.Date;
    
    /// <summary>
    /// The date format to display.
    /// </summary>
    private string? _dateFormat;
    public string? DateFormat
    {
        get => _dateFormat;
        set => SetField(ref _dateFormat, value);
    }
}
