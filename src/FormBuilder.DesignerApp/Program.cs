using FormBuilder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FormBuilder.DesignerApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var formBuilderOptions = new FormBuilderOptions
{
    FormApiHost = builder.Configuration["FormBuilder:FormApiHost"]
};

builder.Services.AddFormBuilder(formBuilderOptions);

await builder.Build().RunAsync();
