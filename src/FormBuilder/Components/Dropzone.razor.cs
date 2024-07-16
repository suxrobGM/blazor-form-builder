using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dropzone component for drag and drop.
/// Wrap any component with this component to make it a dropzone and specify the type of data it accepts and zone name.
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class Dropzone<T> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Zone { get; set; }

    [Parameter]
    public Action<T>? Drop { get; set; }

    private void HandleDrop()
    {
        if (Drop != null && DragDropService.Accepts(Zone))
        {
            Drop((T)DragDropService.Data!);
        }
    }
}
