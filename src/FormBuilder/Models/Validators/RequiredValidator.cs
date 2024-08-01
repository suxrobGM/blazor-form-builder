namespace FormBuilder.Models;

public class RequiredValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Required;
    public override string Text { get; set; } = "Required";
    public bool IsRequired { get; set; } = true;
    public bool ShowRequiredHint { get; set; } = true;
}
