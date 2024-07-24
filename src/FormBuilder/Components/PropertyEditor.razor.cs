using FormBuilder.Models;
using FormBuilder.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

/// <summary>
/// Component that allows editing of field properties in the form builder.
/// </summary>
public partial class PropertyEditor : ComponentBase
{
    private DropDownEnumItem<FieldType>[] _inputTypes = DropDownEnumItem.CreateItems<FieldType>();
    private IEnumerable<string?> _listValues = [];
    private IEnumerable<int> _listIds = [];
    private int _listIdCount;

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject]
    private NotificationService NotificationService { get; set; } = default!;

    #endregion
    
    #region Parameters

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

    #endregion
    
    
    #region Binding Properties
    
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
            FieldTypeChanged.InvokeAsync(new FieldTypeChangedArgs(SelectedField, value));
        }
    }
    
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
    
    private int? SelectedListId
    {
        get => (SelectedField as SelectField)?.ListId;
        set
        {
            if (SelectedField is SelectField selectField && selectField.ListId != value)
            {
                selectField.ListId = value;
                SelectedFieldChanged.InvokeAsync(SelectedField);
                _ = FetchListValuesAsync(value);
            }
        }
    }

    #endregion

    protected override async Task OnInitializedAsync()
    {
        // Load the first 10 list IDs
        await LoadListIdValuesAsync(new LoadDataArgs {Top = 10});
    }

    private async Task LoadListIdValuesAsync(LoadDataArgs args)
    {
        var pagedData = await FormService.GetListIdPagedAsync(args.ToPagedQuery());
        _listIds = pagedData.Data ?? [];
        _listIdCount = pagedData.PageSize * pagedData.PagesCount;
    }
    
    private async Task FetchListValuesAsync(int? listId)
    {
        if (!listId.HasValue)
        {
            return;
        }   
        
        var result = await FormService.GetLovAsync(listId.Value);
            
        if (!result.Success || result.Data is null)
        {
            NotificationService.NotifyError(result.Error);
            return;
        }
        
        _listValues = result.Data.Select(x => x.ListValue);
    }
}

/// <summary>
/// Parameters for the FieldTypeChanged event.
/// </summary>
public record FieldTypeChangedArgs(Field Field, FieldType NewType);
