using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

/// <summary>
/// Component that allows editing of field properties in the form builder.
/// </summary>
public partial class PropertyEditor : ComponentBase
{
    private DropDownEnumItem<FieldType>[] _inputTypes = DropDownEnumItem.CreateItems<FieldType>();
    
    /// <summary>
    /// The currently selected field to edit.
    /// </summary>
    [Parameter]
    public Field? SelectedField { get; set; }
    
    /// <summary>
    /// Event that is triggered when the selected field's property changes.
    /// </summary>
    [Parameter]
    public EventCallback<Field?> SelectedFieldChanged { get; set; }
    
    /// <summary>
    /// Event that is triggered when the field type changes.
    /// </summary>
    [Parameter]
    public EventCallback<FieldTypeChangedArgs> FieldTypeChanged { get; set; }

    
    #region Binding Properties

    private string? _label;
    private string? Label
    {
        get => SelectedField?.Label;
        set
        {
            if (SelectedField is null || SelectedField.Label == value)
            {
                return;
            }
            
            SelectedField.Label = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }
    
    private string? _placeholder;
    private string? Placeholder
    {
        get => SelectedField?.Placeholder;
        set
        {
            if (SelectedField is null || SelectedField.Placeholder == value)
            {
                return;
            }
            
            SelectedField.Placeholder = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }
    
    private FieldType _inputType;
    private FieldType InputType
    {
        get => SelectedField?.Type ?? FieldType.Text;
        set
        {
            if (SelectedField is null || SelectedField.Type == value)
            {
                return;
            }
            
            SelectedField.Type = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
            FieldTypeChanged.InvokeAsync(new FieldTypeChangedArgs
            {
                Field = SelectedField, 
                NewType = value
            });
        }
    }
    
    private bool _required;
    private bool Required
    {
        get => SelectedField?.Required ?? false;
        set
        {
            if (SelectedField is null || SelectedField.Required == value)
            {
                return;
            }
            
            SelectedField.Required = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }
    
    private bool _readOnly;
    private bool ReadOnly
    {
        get => SelectedField?.ReadOnly ?? false;
        set
        {
            if (SelectedField is null || SelectedField.ReadOnly == value)
            {
                return;
            }
            
            SelectedField.ReadOnly = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }
    
    private bool _disabled;
    private bool Disabled
    {
        get => SelectedField?.Disabled ?? false;
        set
        {
            if (SelectedField is null || SelectedField.Disabled == value)
            {
                return;
            }
            
            SelectedField.Disabled = value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }
    
    private int? _selectedListValue;
    private int? SelectedListValue
    {
        get => (SelectedField as SelectField)?.Value;
        set
        {
            if (!value.HasValue || SelectedField is not SelectField selectedField || selectedField.Value == value)
            {
                return;
            }
            
            selectedField.Value = value.Value;
            SelectedFieldChanged.InvokeAsync(SelectedField);
        }
    }

    #endregion
}

/// <summary>
/// Parameters for the FieldTypeChanged event.
/// </summary>
public class FieldTypeChangedArgs : EventArgs
{
    /// <summary>
    /// The field that has changed.
    /// </summary>
    public required Field Field { get; set; }
    
    /// <summary>
    /// The new type of the field.
    /// </summary>
    public required FieldType NewType { get; set; }
}
