namespace FormBuilder.Models;

public class LengthValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Length;
    public override string Text { get; set; } = "Invalid length";
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
}
