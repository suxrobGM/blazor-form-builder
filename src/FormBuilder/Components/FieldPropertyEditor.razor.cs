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
    [Parameter, EditorRequired]
    public Field Field { get; set; } = default!;
    
    [Parameter]
    public EventCallback<Field> FieldChanged { get; set; }
    
    /// <summary>
    /// Event that is triggered when a field type changes.
    /// </summary>
    [Parameter]
    public EventCallback<FieldTypeChangedEventArgs> FieldTypeChanged { get; set; }
    
    /// <summary>
    /// The CSS class for the container element.
    /// </summary>
    [Parameter]
    public string? ContainerClass { get; set; }

    #endregion
    
    #region Binding Properties
    
    private FieldType InputType
    {
        get => Field.Type;
        set
        {
            if (Field.Type == value)
            {
                return;
            }
            
            FieldTypeChanged.InvokeAsync(new FieldTypeChangedEventArgs(Field, value));
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
        var pagedData = await FormService.LovApi.GetListIdPagedAsync(args.ToPagedQuery());
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
        var result = await FormService.LovApi.GetLovAsync(listId.Value);
            
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
