namespace FormBuilder;

public record FormBuilderOptions
{
    public string? FormApiHost { get; init; }
    public TimeSpan CacheExpiration { get; init; } = TimeSpan.FromMinutes(30);
}
