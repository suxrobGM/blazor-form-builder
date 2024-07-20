namespace FormBuilder.API.Configurations;

public static class SwaggerConfiguration
{
    /// <summary>
    /// Configures Swagger for the application.
    /// Adds necessary services to the service collection.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> instance.</param>
    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}
