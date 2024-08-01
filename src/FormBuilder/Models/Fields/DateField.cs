namespace FormBuilder.Models;

/// <summary>
/// Represents a date field.
/// The value is of type DateTime.
/// </summary>
public class DateField : Field<DateTime?>
{
    public override FieldType Type => FieldType.Date;
    public string? DateFormat { get; set; }
}
