namespace FormBuilder;

public record FormBuilderOptions
{
    /// <summary>
    /// The base URL of the form API.
    /// </summary>
    public string? FormApiHost { get; init; }
    
    /// <summary>
    /// The expiration time for caching LOV requests. The default value is 30 minutes.
    /// </summary>
    public TimeSpan CacheExpiration { get; init; } = TimeSpan.FromMinutes(30);
    
    /// <summary>
    /// The theme for the Radzen components. The default value is 'material'.
    /// </summary>
    public string Theme { get; init; } = Themes.Material;
}
