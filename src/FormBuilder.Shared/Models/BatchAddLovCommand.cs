namespace FormBuilder.Shared.Models;

public record BatchAddLovCommand
{
    public IEnumerable<LovDto> Lovs { get; set; } = [];
}
