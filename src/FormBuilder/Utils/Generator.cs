namespace FormBuilder.Utils;

public static class Generator
{
    private static readonly Random Rnd = new();
    
    public static string GenerateShortId(string? prefix = null)
    {
        return $"{prefix}{Rnd.Next():x}";
    }
}
