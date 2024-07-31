using FormBuilder.Shared.Models;

namespace FormBuilder;

public record FormBuilderOptions
{
    public string? FormApiHost { get; init; }
    
    public Func<string, HttpClient, Task<FormDto>>? GetFormById { get; init; }
}
