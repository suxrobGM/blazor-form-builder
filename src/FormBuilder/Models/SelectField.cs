namespace FormBuilder.Models;

/// <summary>
/// Represents a field that allows the user to select from a list of options.
/// The value of this field is a string. The options are defined as the array of strings.
/// </summary>
public class SelectField : Field<string>
{
    public SelectField()
    {
        Type = FieldType.Select;
    }
    
    public int? ListId { get; set; }
    public IEnumerable<string> Options { get; set; } = [];
}
