using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dropzone component for drag and drop.
/// Wrap any component with this component to make it a dropzone and specify the type of data it accepts and zone name.
/// </summary>
/// <typeparam name="TData"></typeparam>
public partial class DropZone<TData> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Zone { get; set; }

    [Parameter]
    public Action<TData>? Drop { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IEnumerable<KeyValuePair<string, object?>>? AdditionalAttributes { get; set; }

    private void HandleDrop()
    {
        if (Drop != null && DragDropService.Accepts(Zone))
        {
            Drop((TData)DragDropService.Data!);
        }
    }
}
