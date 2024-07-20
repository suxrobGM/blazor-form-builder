using FormBuilder.API.Data;

namespace FormBuilder.API.Configurations;

public static class DatabaseConfiguration
{
    /// <summary>
    /// Configures the database for the application.
    /// Adds necessary services to the service collection.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance.</param>
    /// <param name="configSection">
    /// The configuration section from the appsettings.json file that contains the database configuration.
    /// </param>
    public static void ConfigureDatabase(this WebApplicationBuilder builder, string configSection = "DatabaseConfig")
    {
        var dbContextOptions = builder.Configuration.GetRequiredSection(configSection).Get<ApplicationDbContextOptions>()
                               ?? throw new ArgumentException("Could not get a ApplicationDbContextOptions from the json configuration");
        
        builder.Services.AddSingleton(dbContextOptions);
        builder.Services.AddDbContext<ApplicationDbContext>();
    }
}
