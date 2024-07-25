namespace FormBuilder.Shared.Models;

public record BatchDeleteLovCommand
{
    public IEnumerable<int> ListIds { get; set; } = [];
}
