namespace FormBuilder.Shared.Models;

public record UpdateFormDto
{
    public string? FormName { get; set; }
    public string? FormDesign { get; set; }
}
