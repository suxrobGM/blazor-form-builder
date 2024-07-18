namespace FormBuilder.Models;

public class SelectField : Field<int>
{
    public SelectField()
    {
        Type = FieldType.Select;
    }
    
    public List<SelectOption> Options { get; set; } = [];
}
