using FormBuilder.Services;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Dropzone component for drag and drop.
/// Wrap any component with this component to make it a dropzone and specify the type of data it accepts and zone name.
/// </summary>
/// <typeparam name="TData">The type of data that the dropzone accepts.</typeparam>
public partial class DropZone<TData> : ComponentBase
{
    [Inject]
    private DragDropService DragDropService { get; set; } = default!;

    
    #region Paremeters

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The zone name that the dropzone accepts.
    /// </summary>
    [Parameter]
    public string? Zone { get; set; }

    /// <summary>
    /// Event that is triggered when the dropzone accepts the data.
    /// </summary>
    [Parameter]
    public EventCallback<TData> Drop { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IEnumerable<KeyValuePair<string, object?>>? AdditionalAttributes { get; set; }

    #endregion
    

    private void HandleDrop()
    {
        if (DragDropService.Accepts(Zone))
        {
            Drop.InvokeAsync((TData)DragDropService.Data!);
        }
    }
}
