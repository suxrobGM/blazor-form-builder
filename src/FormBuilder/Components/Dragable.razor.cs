using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dragable component for drag and drop.
/// You can wrap any component with this component to make it draggable.
/// </summary>
/// <typeparam name="TData">The type of data that the dragable component holds.</typeparam>
public partial class Dragable<TData> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;

    
    #region Parameters

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The zone name that the dragable component belongs to.
    /// </summary>
    [Parameter]
    public string? Zone { get; set; }

    /// <summary>
    /// The data that the dragable component holds.
    /// </summary>
    [Parameter]
    public TData? Data { get; set;}
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IEnumerable<KeyValuePair<string, object?>>? AdditionalAttributes { get; set; }

    #endregion
    

    private void HandleDragStart()
    {
        DragDropService.StartDrag(Data, Zone);
    }
}
