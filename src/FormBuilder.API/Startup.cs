using FormBuilder.API.Configurations;
using FormBuilder.API.Services;
using Serilog;

namespace FormBuilder.API;

public static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureLogger();
        builder.ConfigureCors();
        builder.ConfigureControllers();
        builder.ConfigureSwagger();
        builder.ConfigureDatabase();
        builder.Services.AddScoped<FormService>();
        builder.Services.AddScoped<LovService>();
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
        
        app.UseCors("AnyCors");
        app.UseHttpsRedirection();
        app.MapControllers();
        return app;
    }
}
