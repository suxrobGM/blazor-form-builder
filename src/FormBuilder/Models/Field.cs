namespace FormBuilder.Models;

public class Field
{
    public string? Name { get; set; }
    public string? Label { get; set; }
    public FieldType Type { get; set; }
    public object? Value { get; set; }
    public bool Required { get; set; }
    public List<SelectItem> Options { get; set; } = [];
    public int? ListId { get; set; }
}
