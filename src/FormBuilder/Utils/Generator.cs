namespace FormBuilder.Utils;

public static class Generator
{
    private static readonly Random Rnd = new();
    
    /// <summary>
    /// Generates a short unique identifier in the form of hex string.
    /// </summary>
    /// <param name="prefix">An optional prefix that will be added to the generated identifier.</param>
    public static string GenerateShortId(string? prefix = null)
    {
        return $"{prefix}{Rnd.Next():x}";
    }
}
