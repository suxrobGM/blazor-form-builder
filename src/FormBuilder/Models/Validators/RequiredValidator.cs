namespace FormBuilder.Models;

public class RequiredValidator : Validator
{
    public override ValidatorType Type => ValidatorType.Required;
    
    private string _text = "Required";
    public override string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    
    /// <summary>
    /// Whether the field is required or not.
    /// </summary>
    private bool _isRequired = true;
    public bool IsRequired
    {
        get => _isRequired;
        set => SetField(ref _isRequired, value);
    }
    
    /// <summary>
    /// Whether to show a hint message that the field is required.
    /// </summary>
    private bool _showRequiredHint = true;

    public bool ShowRequiredHint
    {
        get => _showRequiredHint;
        set => SetField(ref _showRequiredHint, value);
    }
}
