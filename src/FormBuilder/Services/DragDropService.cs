namespace FormBuilder.Services;

/// <summary>
/// Service for drag and drop operations.
/// </summary>
public class DragDropService
{
    /// <summary>
    /// The data that is being dragged.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// The zone that the data belongs to.
    /// </summary>
    public string? Zone { get; set; }

    /// <summary>
    /// Starts the drag operation.
    /// </summary>
    /// <param name="data">The data that is being dragged.</param>
    /// <param name="zone">The zone that the data belongs to.</param>
    public void StartDrag(object? data, string? zone)
    {
        Data = data;
        Zone = zone;
    }

    /// <summary>
    /// Checks if the dropzone accepts the data.
    /// </summary>
    /// <param name="zone">The zone name of the dropzone.</param>
    /// <returns>True if the dropzone accepts the data; otherwise, false.</returns>
    public bool Accepts(string? zone)
    {
        return Zone == zone && Data is not null;
    }
}
