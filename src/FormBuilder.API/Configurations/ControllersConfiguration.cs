namespace FormBuilder.API.Configurations;

public static class ControllersConfiguration
{
    /// <summary>
    /// Configures controllers for the application.
    /// Adds necessary services to the service collection.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance.</param>
    public static void ConfigureControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
    }
}
