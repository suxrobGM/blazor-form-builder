namespace FormBuilder.Models;

public class RangeValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Range;
    public override string Text { get; set; } = "Not in the valid range";
    public int Min { get; set; }
    public int Max { get; set; }
}
