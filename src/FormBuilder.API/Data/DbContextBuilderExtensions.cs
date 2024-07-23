using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Data;

internal static class DbContextBuilderExtensions
{
    /// <summary>
    /// Configures the DbContextOptionsBuilder to use SQL Server as the database provider.
    /// </summary>
    /// <param name="options">The DbContextOptionsBuilder to configure.</param>
    /// <param name="connectionString">The connection string to use for the database connection.</param>
    /// <returns>The configured DbContextOptionsBuilder.</returns>
    public static DbContextOptionsBuilder ConfigureSqlServer(this DbContextOptionsBuilder options, string connectionString)
    {
        options.UseSqlServer(connectionString);
        return options;
    }
}
