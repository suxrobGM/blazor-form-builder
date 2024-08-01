namespace FormBuilder.Models;

public class EmailValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Email;
    public override string Text { get; set; } = "Invalid email address";
}
