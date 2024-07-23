using FormBuilder.Models;
using FormBuilder.Services;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

public partial class LoadFormDialog : ComponentBase
{
    private FormDto _formModel = new();
    private bool _isLoading;

    #region Injected Servicse

    [Inject]
    private NotificationService NotificationService { get; set; } = default!;
    
    [Inject]
    private FormService FormService { get; set; } = default!;

    #endregion

    
    #region Parameters

    [Parameter]
    public EventCallback<FormCreatedEventArgs> FormLoaded { get; set; }

    #endregion
    
    private bool EitherIdOrDesignRequired()
    {
        return !string.IsNullOrEmpty(_formModel.Id) || !string.IsNullOrEmpty(_formModel.FormDesign);
    }

    private bool ValidateFormDesign()
    {
        if (string.IsNullOrEmpty(_formModel.FormDesign))
        {
            return true; // No form design to validate
        }
        
        var formDefinition = FormService.DeserializeFormDesign(_formModel.FormDesign);
        return formDefinition is not null;
    }
    
    private Task LoadFormAsync()
    {
        if (!string.IsNullOrEmpty(_formModel.Id))
        {
            return LoadFormByIdAsync(_formModel.Id);
        }
        
        if (!string.IsNullOrEmpty(_formModel.FormDesign))
        {
            return HandleFormDesignDeserialization(_formModel.FormDesign, null);
        }
        
        return Task.CompletedTask;
    }
    
    private async Task LoadFormByIdAsync(string id)
    {
        _isLoading = true;
        _formModel.Id = id;
        var result = await FormService.GetFormByIdAsync(_formModel.Id);
        
        if (result is { Success: true, Data.FormDesign: not null })
        {
            await HandleFormDesignDeserialization(result.Data.FormDesign, _formModel.Id);
        }
        else
        {
            NotificationService.NotifyError(result.Error!);
        }
        
        _isLoading = false;
    }
    
    private async Task HandleFormDesignDeserialization(string formDesign, string? id)
    {
        var formDefinition = FormService.DeserializeFormDesign(formDesign);
        if (formDefinition != null)
        {
            await FormLoaded.InvokeAsync(new FormCreatedEventArgs(id, formDefinition));
            NotificationService.NotifySuccess("Form loaded successfully.");
        }
        else
        {
            NotificationService.NotifyError("Failed to deserialize form design.");
        }
    }
}

public record FormCreatedEventArgs(string? FormId, FormDefinition FormDefinition);
