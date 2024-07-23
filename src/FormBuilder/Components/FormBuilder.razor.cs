using FormBuilder.Factories;
using FormBuilder.Models;
using FormBuilder.Services;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace FormBuilder.Components;

/// <summary>
/// Component that allows building forms by adding and removing inputs.
/// </summary>
public partial class FormBuilder : ComponentBase
{
    private FormDefinition _formDefinition = new();
    private Field? _selectedField;
    private string _formDesignJson = "{}";
    private string? _formId;
    private bool _isLoading;

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;
    
    [Inject]
    private DialogService DialogService { get; set; } = default!;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await UpdateFormDesignJsonAsync();
    }
    
    /// <summary>
    /// Updates the form design JSON string when the form definition changes.
    /// </summary>
    private async Task UpdateFormDesignJsonAsync()
    {
        _formDesignJson = await FormService.SerializeFormDesignAsync(_formDefinition, true);
    }

    private Task AddField(FieldType fieldType)
    {
        var field = FieldFactory.CreateField(fieldType);
        field.Label = fieldType.ToString();
        _formDefinition.Fields.Add(field);
        SelectField(field);
        return UpdateFormDesignJsonAsync();
    }
    
    private Task RemoveField(Field field)
    {
        _formDefinition.Fields.Remove(field);
        _selectedField = null;
        return UpdateFormDesignJsonAsync();
    }

    private void SelectField(Field field)
    {
        _selectedField = field;
        StateHasChanged();
    }
    
    /// <summary>
    /// Changes the field type if the new field type is different from the current one.
    /// Creates a new field with the new type and copies the properties from the old field.
    /// </summary>
    /// <param name="args">Event parameters</param>
    private Task HandleFieldTypeChanged(FieldTypeChangedArgs args)
    {
        var field = args.Field;
        var newField = FieldFactory.CreateField(args.NewType);
        newField.Label = field.Label;
        newField.Placeholder = field.Placeholder;
        newField.Required = field.Required;
        newField.ReadOnly = field.ReadOnly;
        newField.Disabled = field.Disabled;
        
        var index = _formDefinition.Fields.IndexOf(field);
        _formDefinition.Fields[index] = newField; // Replace the old field with the new one, old one will be deleted by GC
        SelectField(newField);
        return UpdateFormDesignJsonAsync();
    }
    
    private Task SwapFields(Field targetField, Field droppedField)
    {
        var targetIndex = _formDefinition.Fields.IndexOf(targetField);
        var droppedIndex = _formDefinition.Fields.IndexOf(droppedField);
        var fields = _formDefinition.Fields;
        (fields[targetIndex], fields[droppedIndex]) = (fields[droppedIndex], fields[targetIndex]);
        return UpdateFormDesignJsonAsync();
    }
    
    private async Task SaveFormAsync()
    {
        if (_formDefinition.Fields.Count == 0)
        {
            NotificationService.NotifyWarning("Cannot save an empty form");
            return;
        }
        
        _isLoading = true;
        Result result;
        
        if (string.IsNullOrEmpty(_formId))
        {
            var createFormResult = await FormService.CreateFormAsync(_formDefinition);
            result = createFormResult;
            _formId = createFormResult is { Success: true, Data: not null } ? createFormResult.Data.Id : null;
        }
        else
        {
            result = await FormService.UpdateFormAsync(_formId, _formDefinition);
        }
        
        if (result.Success)
        {
            NotificationService.NotifySuccess("Form saved successfully");
        }
        else
        {
            NotificationService.NotifyError(result.Error);
        }
        
        _isLoading = false;
    }

    private Task OpenLoadFormDialogAsync()
    {
        return DialogService.OpenAsync<LoadFormDialog>("Load Form", new Dictionary<string, object>
        {
            { "FormLoaded", EventCallback.Factory.Create<FormCreatedEventArgs>(this, LoadForm) }
        });
    }
    
    private Task LoadForm(FormCreatedEventArgs args)
    {
        _formId = args.FormId;
        _formDefinition = args.FormDefinition;
        return UpdateFormDesignJsonAsync();
    }
}
