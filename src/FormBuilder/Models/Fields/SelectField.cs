namespace FormBuilder.Models;

/// <summary>
/// Represents a field that allows the user to select from a list of options.
/// The value of this field is a string. The options are defined as the array of strings.
/// </summary>
public class SelectField : Field<string>
{
    public override FieldType Type => FieldType.Select;
    
    /// <summary>
    /// The list ID that corresponds to the list of options for this field.
    /// </summary>
    private int? _listId;
    public int? ListId
    {
        get => _listId;
        set => SetField(ref _listId, value);
    }
    
    /// <summary>
    /// Associated list values for the particular list ID.
    /// </summary>
    private IEnumerable<string> _options = [];
    public IEnumerable<string> Options
    {
        get => _options;
        set => SetField(ref _options, value);
    }
}
