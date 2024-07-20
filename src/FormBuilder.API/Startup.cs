using FormBuilder.API.Configurations;
using Serilog;

namespace FormBuilder.API;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureLogger();
        builder.ConfigureControllers();
        builder.ConfigureSwagger();
        builder.ConfigureDatabase();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}
