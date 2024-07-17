namespace FormBuilder.Models;

public class SelectField : Field<int>
{
    public SelectField()
    {
        Type = FieldType.Select;
    }
    
    public List<SelectItem> Options { get; set; } = [];
}
