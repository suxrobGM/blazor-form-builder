using FormBuilder.Models;
using FormBuilder.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

public partial class FormRenderer : ComponentBase
{
    private FormDefinition _formDefinition = new();
    private bool _isLoading;

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;

    #endregion

    #region Parameters

    [Parameter]
    public string? FormId { get; set; }
    
    [Parameter]
    public string? FormJson { get; set; }
    
    [Parameter]
    public EventCallback<string?> FormIdChanged { get; set; }
    
    [Parameter]
    public EventCallback<string?> FormJsonChanged { get; set; }

    #endregion

    private string GetFormTitle()
    {
        return $"Form: {_formDefinition.Name}, ID: {FormId}";
    }
    
    private async Task UpdateFormDesignJsonAsync()
    {
        FormJson = await FormService.SerializeFormDesignAsync(_formDefinition, true);
    }
    
    private Task LoadForm(FormLoadedEventArgs args)
    {
        FormId = args.FormId;
        _formDefinition = args.FormDefinition;
        return UpdateFormDesignJsonAsync();
    }
}
