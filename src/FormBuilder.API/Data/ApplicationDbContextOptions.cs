namespace FormBuilder.API.Data;

/// <summary>
/// The options for configuring the application database context.
/// </summary>
public class ApplicationDbContextOptions
{
    private const string DefaultConnectionString = "Data Source=localhost;Port=5432;Database=FormBuilderDB;Username=postgres;Password=postgres";
    
    /// <summary>
    /// The connection string to the database.
    /// If not specified, the default connection string will be used.
    /// </summary>
    public string? ConnectionString { get; set; } = DefaultConnectionString;
}
