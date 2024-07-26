using FormBuilder.Models;
using FormBuilder.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

public partial class FormRenderer : ComponentBase
{
    private FormDefinition _formDefinition = new();
    private string? _errorMessage;

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;

    #endregion

    
    #region Parameters

    /// <summary>
    /// Form ID to load from API
    /// </summary>
    [Parameter]
    public string? FormId { get; set; }
    
    /// <summary>
    /// Form JSON to load directly into the form renderer.
    /// </summary>
    [Parameter]
    public string? FormJson { get; set; }
    
    /// <summary>
    /// Whether the form is currently loading from the API or JSON.
    /// </summary>
    [Parameter]
    public bool IsLoadingForm { get; set; }
    
    /// <summary>
    /// Event that is triggered when the IsLoadingForm property changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsLoadingFormChanged { get; set; }
    
    /// <summary>
    /// Event that is triggered when the form is loaded and ready to be rendered.
    /// </summary>
    [Parameter]
    public EventCallback<FormDefinition> FormLoaded { get; set; }

    #endregion

    
    #region Binding Properties
    
    private bool IsLoading
    {
        get => IsLoadingForm;
        set
        {
            if (IsLoadingForm == value)
            {
                return;
            }
            
            IsLoadingForm = value;
            IsLoadingFormChanged.InvokeAsync(value);
            StateHasChanged();
        }
    }

    #endregion
    
    
    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(FormId))
        {
            await LoadFormFromIdAsync(FormId);
        }
        else if (!string.IsNullOrEmpty(FormJson))
        {
            await LoadFormFromJsonAsync(FormJson);
        }
    }
    
    /// <summary>
    /// Loads the form from the API using the provided form ID.
    /// </summary>
    /// <param name="formId">The form ID to load from the API.</param>
    public async Task LoadFormFromIdAsync(string formId)
    {
        IsLoading = true;
        var result = await FormService.GetFormByIdAsync(formId);
        
        if (result is { Success: true, Data.FormDesign: not null })
        {
            await LoadFormFromJsonAsync(result.Data.FormDesign);
        }
        else
        {
            _errorMessage = "Failed to load form from API.";
        }
        
        IsLoading = false;
    }
    
    /// <summary>
    /// Loads the form directly from the provided JSON string.
    /// </summary>
    /// <param name="formJson">The JSON string representing the form to load.</param>
    public async Task LoadFormFromJsonAsync(string formJson)
    {
        IsLoading = true;
        var formDefinition = await FormService.DeserializeFormAsync(formJson);
        
        if (formDefinition is null)
        {
            _errorMessage = "Failed to load form from JSON.";
            IsLoading = false;
            return;
        }
        
        _errorMessage = null;
        _formDefinition = formDefinition;
        await PopulateSelectFieldOptions();
        await FormLoaded.InvokeAsync(_formDefinition);
        IsLoading = false;
    }
    
    /// <summary>
    /// Gets the FormDefinition object as a JSON string.
    /// </summary>
    public Task<string> GetFormAsJsonAsync()
    {
        return FormService.SerializeFormAsync(_formDefinition);
    }
    
    private string GetFormTitle()
    {
        return $"Form: {_formDefinition.Name}, ID: {_formDefinition.Id}";
    }
    
    /// <summary>
    /// Fetches the list of values from the API for the select fields.
    /// </summary>
    private async Task PopulateSelectFieldOptions()
    {
        foreach (var field in _formDefinition.Fields)
        {
            if (field is not SelectField { ListId: not null } selectField)
            {
                continue;
            }
            
            var result = await FormService.GetLovAsync(selectField.ListId.Value);
                
            if (!result.Success || result.Data is null)
            {
                NotificationService.NotifyWarning($"Failed to load list of values for the List ID: {selectField.ListId}");
            }
            else
            {
                selectField.Options = result.Data.Where(i => !string.IsNullOrEmpty(i.ListValue)).Select(x => x.ListValue)!;
            }
        }
    }
}
