namespace FormBuilder.Shared.Models;

public record CreateFormDto
{
    public string? FormName { get; set; }
    public string? FormDesign { get; set; }
}
