namespace FormBuilder.Models;

public class EmailValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Email;
    
    private string _text = "Invalid email address";
    public override string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
}
