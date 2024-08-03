using FormBuilder;
using FormBuilder.DesignerApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var formBuilderOptions = new FormBuilderOptions
{
    FormApiHost = builder.Configuration["FormBuilder:FormApiHost"],
    Theme = Themes.Material
};

builder.Services.AddFormBuilder(formBuilderOptions);

await builder.Build().RunAsync();
