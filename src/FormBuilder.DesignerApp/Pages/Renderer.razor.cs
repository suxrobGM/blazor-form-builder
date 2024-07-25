using FormBuilder.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.DesignerApp.Pages;

public partial class Renderer : ComponentBase
{
    private string? _formId;
    private string? _formJson;
    private bool _isLoading;
    
    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;

    #endregion
    
    private Task LoadFormAsync()
    {
        if (!string.IsNullOrEmpty(_formId))
        {
            return LoadFormByIdAsync(_formId);
        }
        
        if (!string.IsNullOrEmpty(_formJson))
        {
            return HandleFormDeserialization(_formJson);
        }
        
        return Task.CompletedTask;
    }
    
    private async Task LoadFormByIdAsync(string id)
    {
        _isLoading = true;
        var result = await FormService.GetFormByIdAsync(id);
        
        if (result is { Success: true, Data.FormDesign: not null })
        {
            await HandleFormDeserialization(result.Data.FormDesign);
        }
        else
        {
            NotificationService.NotifyError(result.Error!);
        }
        
        _isLoading = false;
    }
    
    private async Task HandleFormDeserialization(string formDesign)
    {
        var formDefinition = await FormService.DeserializeFormDesignAsync(formDesign);
        if (formDefinition != null)
        {
            NotificationService.NotifySuccess("Form loaded successfully.");
        }
        else
        {
            NotificationService.NotifyError("Failed to deserialize form design.");
        }
    }
}
