using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class PropertyEditor : ComponentBase
{
    [Parameter]
    public Field? SelectedField { get; set; }
    
    [Parameter]
    public EventCallback<Field?> SelectedFieldChanged { get; set; }

    
    #region Binding Properties

    private string? _label;
    private string? Label
    {
        get => SelectedField?.Label;
        set
        {
            if (SelectedField is null)
            {
                return;
            }
            
            SelectedField.Label = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }

    #endregion
}
