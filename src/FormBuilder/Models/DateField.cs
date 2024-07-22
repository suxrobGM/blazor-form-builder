namespace FormBuilder.Models;

/// <summary>
/// Represents a date field.
/// The value is of type DateTime.
/// </summary>
public class DateField : Field<DateTime>
{
    public DateField()
    {
        Type = FieldType.Date;
    }
}
