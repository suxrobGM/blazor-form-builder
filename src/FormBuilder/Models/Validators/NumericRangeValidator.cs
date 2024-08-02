namespace FormBuilder.Models;

public class NumericRangeValidator : Validator
{
    public override ValidatorType Type => ValidatorType.NumericRange;
    
    private string _text = "Not in the valid range";
    public override string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    
    /// <summary>
    /// The minimum integer value that the numeric field value must have.
    /// </summary>
    private int _min;

    public int Min
    {
        get => _min;
        set => SetField(ref _min, value);
    }
    
    /// <summary>
    /// The maximum integer value that the numeric field value must have.
    /// </summary>
    private int _max;

    public int Max
    {
        get => _max;
        set => SetField(ref _max, value);
    }
}
