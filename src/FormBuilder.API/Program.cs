using FormBuilder.API;
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
    
    app.Run();
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
