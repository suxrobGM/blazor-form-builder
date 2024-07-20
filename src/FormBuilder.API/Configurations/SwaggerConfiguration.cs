using System.Reflection;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FormBuilder.API",
                Version = "v1"
            });
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
