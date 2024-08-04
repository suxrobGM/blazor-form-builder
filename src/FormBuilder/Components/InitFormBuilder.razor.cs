using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Initializes the overlay Radzen components such as the dialog, notification, and tooltip and loads the static files.
/// It also sets the theme for the Radzen components.
/// Use this component only in the layout or root component. For example, in the `MainLayout.razor` file.
/// </summary>
[SupportedOSPlatform("browser")]
public partial class InitFormBuilder : ComponentBase
{
    [Inject]
    private FormBuilderOptions Options { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        // Use relative path for GitHub Pages
        var basePath = Environment.GetEnvironmentVariable("GH_PAGES") == "true" ? "./" : "/"; 
        
        await JSHost.ImportAsync(nameof(InitFormBuilder), 
            $"{basePath}_content/FormBuilder/Components/InitFormBuilder.js");

        LoadStaticFiles();
    }

    [JSImport("loadStaticFiles", nameof(InitFormBuilder))]
    internal static partial void LoadStaticFiles();
}
