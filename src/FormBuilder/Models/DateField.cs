namespace FormBuilder.Models;

public class DateField : Field<DateTime>
{
    public DateField()
    {
        Type = FieldType.Date;
    }
}
