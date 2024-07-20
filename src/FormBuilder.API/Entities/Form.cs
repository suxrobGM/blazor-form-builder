namespace FormBuilder.API.Entities;

public class Form
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? FormName { get; set; }
    public string? FormDesign { get; set; }
}
