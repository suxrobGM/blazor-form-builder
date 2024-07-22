using FormBuilder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FormBuilder.DesignerApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var formBuilderOptions = new FormBuilderOptions
{
    FormApiUrl = builder.Configuration["FormBuilderOptions:FormApiUrl"]
};

builder.Services.AddFormBuilder(formBuilderOptions);

await builder.Build().RunAsync();