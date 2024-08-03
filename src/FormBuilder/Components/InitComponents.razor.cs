using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Initializes the overlay Radzen components such as the dialog, notification, and tooltip and loads the static files.
/// It also sets the theme for the Radzen components.
/// </summary>
[SupportedOSPlatform("browser")]
public partial class InitComponents : ComponentBase
{
    [Inject]
    private FormBuilderOptions Options { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        await JSHost.ImportAsync(nameof(InitComponents), 
            "/_content/FormBuilder/Components/InitComponents.js");

        LoadStaticFiles();
    }

    [JSImport("loadStaticFiles", nameof(InitComponents))]
    internal static partial void LoadStaticFiles();
}
