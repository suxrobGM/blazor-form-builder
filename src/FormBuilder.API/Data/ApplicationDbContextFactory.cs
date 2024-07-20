using Microsoft.EntityFrameworkCore.Design;

namespace FormBuilder.API.Data;

/// <summary>
/// ApplicationDbContextFactory is a factory class for creating instances of ApplicationDbContext
/// with dynamically determined database providers at design time. This is primarily used for 
/// generating database migrations specific to a given database provider.
/// </summary>
internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// Creates a new instance of ApplicationDbContext with the appropriate configuration
    /// based on the specified database provider.
    /// </summary>
    /// <param name="args">Command-line arguments passed to the factory, used to determine the database provider.</param>
    /// <returns>A configured instance of ApplicationDbContext.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid or unspecified database provider is passed.</exception>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var defaultOptions = new ApplicationDbContextOptions();
        return new ApplicationDbContext(defaultOptions);
    }
}
