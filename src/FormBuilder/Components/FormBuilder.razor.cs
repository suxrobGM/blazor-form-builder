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
    private string? _formId;
    private bool _isLoading;

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;

    #endregion
    
    
    private void AddField(FieldType fieldType)
    {
        var field = FieldFactory.CreateField(fieldType);
        field.Label = fieldType.ToString();

        _formDefinition.Fields.Add(field);
        SelectField(field);
    }
    
    private void RemoveField(Field field)
    {
        _formDefinition.Fields.Remove(field);
        _selectedField = null;
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
    private void HandleFieldTypeChanged(FieldTypeChangedArgs args)
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
    }

    private void HandleDropField(Action addFieldFn)
    {
        addFieldFn();
    }
    
    private void SwapFields(Field targetField, Field droppedField)
    {
        var targetIndex = _formDefinition.Fields.IndexOf(targetField);
        var droppedIndex = _formDefinition.Fields.IndexOf(droppedField);
        var fields = _formDefinition.Fields;
        (fields[targetIndex], fields[droppedIndex]) = (fields[droppedIndex], fields[targetIndex]);
    }
    
    private async Task SaveFormAsync()
    {
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
            NotificationService.Notify(NotificationSeverity.Success, "Form saved successfully");
        }
        else
        {
            NotificationService.Notify(NotificationSeverity.Error, result.Error);
        }
        
        _isLoading = false;
    }
}
