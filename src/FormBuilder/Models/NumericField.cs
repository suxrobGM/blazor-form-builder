namespace FormBuilder.Models;

public class NumericField<T> : Field<T> where T : struct
{
    public NumericField()
    {
        if (typeof(T) == typeof(int) || typeof(T) == typeof(long) || typeof(T) == typeof(short))
        {
            Type = FieldType.NumericInt;
        }
        else if (typeof(T) == typeof(uint) || typeof(T) == typeof(ulong) || typeof(T) == typeof(ushort))
        {
            Type = FieldType.NumericInt;
            Min = 0;
        }
        else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(double) || typeof(T) == typeof(float))
        {
            Type = FieldType.NumericDecimal;
        }
        else
        {
            throw new InvalidOperationException("Unsupported numeric type.");
        }
    }
    
    public override FieldType Type { get; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public string Step { get; set; } = "1";
    public bool ShowUpDown { get; set; } = true;
    public string? Format { get; set; }
}
