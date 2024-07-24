namespace FormBuilder.Shared.Models;

public record CreateFormCommand
{
    public string? FormName { get; set; }
    public string? FormDesign { get; set; }
}
