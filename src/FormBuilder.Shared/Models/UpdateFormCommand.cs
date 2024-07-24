namespace FormBuilder.Shared.Models;

public record UpdateFormCommand
{
    public string? FormName { get; set; }
    public string? FormDesign { get; set; }
}
