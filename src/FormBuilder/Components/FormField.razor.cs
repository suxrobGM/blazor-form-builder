using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Represents a form field component.
/// </summary>
public partial class FormField : ComponentBase
{
    /// <summary>
    /// The field to be edited.
    /// </summary>
    [Parameter, EditorRequired]
    public Field Field { get; set; } = default!;
    
    /// <summary>
    /// Whether the field is disabled for editing in the form builder.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }
    
    /// <summary>
    /// The content to be appended to the field (added after the input).
    /// You can use this to add additional content to the field like icons or buttons.
    /// </summary>
    [Parameter]
    public RenderFragment? Append { get; set; }
    
    /// <summary>
    /// The content to be prepended to the field (added before the input).
    /// You can use this to add additional content to the field like icons or buttons.
    /// </summary>
    [Parameter]
    public RenderFragment? Prepend { get; set; }
}
