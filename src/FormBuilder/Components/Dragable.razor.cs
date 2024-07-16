using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dragable component for drag and drop.
/// You can wrap any component with this component to make it draggable.
/// </summary>
/// <typeparam name="T"></typeparam>
public partial class Dragable<T> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;
    
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Zone { get; set; }

    [Parameter]
    public T? Data { get; set;}

    private void HandleDragStart()
    {
        DragDropService.StartDrag(Data, Zone);
    }
}
