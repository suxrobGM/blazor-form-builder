namespace FormBuilder.Models;

public class NumericRangeValidator : Validator
{
    public override ValidatorType Type => ValidatorType.NumericRange;
    public override string Text { get; set; } = "Not in the valid range";
    public int Min { get; set; }
    public int Max { get; set; }
}
