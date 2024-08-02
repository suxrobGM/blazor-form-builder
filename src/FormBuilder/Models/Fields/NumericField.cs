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
    
    /// <summary>
    /// The step value for the numeric field.
    /// </summary>
    private string _step = "1";
    public string Step
    { 
        get => _step;
        set => SetField(ref _step, value);
    }

    /// <summary>
    /// Whether to show the up and down buttons.
    /// </summary>
    private bool _showUpDown = true;
    public bool ShowUpDown
    {
        get => _showUpDown;
        set => SetField(ref _showUpDown, value);
    }
    
    /// <summary>
    /// The format to display the numeric value.
    /// </summary>
    private string? _format;
    public string? Format
    {
        get => _format;
        set => SetField(ref _format, value);
    }
    
    public decimal? GetMin()
    {
        return Validators.OfType<NumericRangeValidator>().FirstOrDefault()?.Min;
    }
    
    public decimal? GetMax()
    {
        return Validators.OfType<NumericRangeValidator>().FirstOrDefault()?.Max;
    }
}
