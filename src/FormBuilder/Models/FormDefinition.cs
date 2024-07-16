namespace FormBuilder.Models;

public class FormDefinition
{
    public string? Name { get; set; }
    public List<Field> Fields { get; set; } = [];
}
