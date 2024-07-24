using FormBuilder.API;
using FormBuilder.API.Data;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up");

    var app = WebApplication.CreateBuilder(args)
        .ConfigureServices()
        .ConfigurePipeline();

    if (args.Contains("--seed"))
    {
        using var serviceScope = app.Services.CreateScope();
        await SeedData.InitializeAsync(serviceScope.ServiceProvider);
    }
    else
    {
        app.Run();
    }
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
