namespace FormBuilder.Models;

public class LengthValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Length;
    
    private string _text = "Invalid length";
    public override string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    
    /// <summary>
    /// The minimum length of characters that the text field value must have.
    /// </summary>
    private int? _minLength;
    public int? MinLength
    {
        get => _minLength;
        set => SetField(ref _minLength, value);
    }
    
    /// <summary>
    /// The maximum length of characters that the text field value must have.
    /// </summary>
    private int? _maxLength;
    public int? MaxLength
    {
        get => _maxLength;
        set => SetField(ref _maxLength, value);
    }
}
