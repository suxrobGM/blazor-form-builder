using FormBuilder.Models;
using FormBuilder.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

/// <summary>
/// Component that allows editing of field properties in the form builder.
/// </summary>
public partial class FieldPropertyEditor : ComponentBase
{
    private DropDownEnumItem<FieldType>[] _inputTypes = DropDownEnumItem.CreateItems<FieldType>();
    private IEnumerable<string?> _listValues = [];
    private IEnumerable<int> _listIds = [];
    private int _listIdCount;
    private bool _fetchedInitialListIds;

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
    public Field? Field { get; set; }
    
    /// <summary>
    /// Event that is triggered when the selected field's property changes.
    /// </summary>
    [Parameter]
    public EventCallback<Field?> FieldChanged { get; set; }
    
    /// <summary>
    /// Event that is triggered when a field property changes such as label, placeholder, etc.
    /// </summary>
    [Parameter]
    public EventCallback<FieldPropertyChangedArgs> PropertyChanged { get; set; }

    #endregion
    
    
    #region Binding Properties
    
    private string? Label
    {
        get => Field?.Label;
        set
        {
            if (Field is null || Field.Label == value)
            {
                return;
            }
            
            Field.Label = value;
            FieldChanged.InvokeAsync(Field);
            PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(Models.Field.Label), value));
        }
    }
    
    private string? Placeholder
    {
        get => Field?.Placeholder;
        set
        {
            if (Field is null || Field.Placeholder == value)
            {
                return;
            }
            
            Field.Placeholder = value;
            FieldChanged.InvokeAsync(Field);
            PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(Models.Field.Placeholder), value));
        }
    }
    
    private FieldType InputType
    {
        get => Field?.Type ?? FieldType.Text;
        set
        {
            if (Field is null || Field.Type == value)
            {
                return;
            }
            
            FieldChanged.InvokeAsync(Field);
            PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(Models.Field.Type), value));
        }
    }
    
    private bool ReadOnly
    {
        get => Field?.ReadOnly ?? false;
        set
        {
            if (Field is null || Field.ReadOnly == value)
            {
                return;
            }
            
            Field.ReadOnly = value;
            FieldChanged.InvokeAsync(Field);
            PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(Models.Field.ReadOnly), value));
        }
    }
    
    private bool Disabled
    {
        get => Field?.Disabled ?? false;
        set
        {
            if (Field is null || Field.Disabled == value)
            {
                return;
            }
            
            Field.Disabled = value;
            FieldChanged.InvokeAsync(Field);
            PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(Models.Field.Disabled), value));
        }
    }
    
    private int? SelectedListId
    {
        get => (Field as SelectField)?.ListId;
        set
        {
            if (Field is SelectField selectField && selectField.ListId != value)
            {
                selectField.ListId = value;
                FieldChanged.InvokeAsync(Field);
                PropertyChanged.InvokeAsync(new FieldPropertyChangedArgs(Field, nameof(SelectField.ListId), value));
                _ = FetchListValuesAsync(value);
            }
        }
    }

    private bool _listValuesLoading;
    private bool ListValuesLoading
    {
        get => _listValuesLoading;
        set
        {
            if (_listValuesLoading == value)
            {
                return;
            }
            
            _listValuesLoading = value;
            StateHasChanged();
        }
    }

    #endregion

    protected override async Task OnParametersSetAsync()
    {
        if (Field is SelectField && !_fetchedInitialListIds)
        {
            // Load the first 10 list IDs after the field is set and only once
            await LoadListIdValuesAsync(new LoadDataArgs {Top = 10});
            _fetchedInitialListIds = true;
        }
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
        
        ListValuesLoading = true;
        var result = await FormService.GetLovAsync(listId.Value);
            
        if (!result.Success || result.Data is null)
        {
            NotificationService.NotifyError(result.Error);
        }
        else
        {
            _listValues = result.Data.Select(x => x.ListValue);
        }
        
        ListValuesLoading = false;
    }
}

public record FieldPropertyChangedArgs(Field Field, string PropertyName, object? NewValue);
