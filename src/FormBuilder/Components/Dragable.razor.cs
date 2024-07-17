using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dragable component for drag and drop.
/// You can wrap any component with this component to make it draggable.
/// </summary>
/// <typeparam name="TData"></typeparam>
public partial class Dragable<TData> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Zone { get; set; }

    [Parameter]
    public TData? Data { get; set;}
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IEnumerable<KeyValuePair<string, object?>>? AdditionalAttributes { get; set; }

    private void HandleDragStart()
    {
        DragDropService.StartDrag(Data, Zone);
    }
}
