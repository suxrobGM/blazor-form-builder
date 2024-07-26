﻿using FormBuilder.Factories;
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
    private string _formDesignJson = "{}";

    #region Injected Services

    [Inject]
    private FormService FormService { get; set; } = default!;
    
    [Inject] 
    private NotificationService NotificationService { get; set; } = default!;
    
    [Inject]
    private DialogService DialogService { get; set; } = default!;

    #endregion

    #region Binding Properties

    private Field? _selectedField;
    private Field? SelectedField
    {
        get => _selectedField;
        set
        {
            _selectedField = value;
            StateHasChanged();
        }
    }
    
    private bool _isLoading;
    private bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading == value)
            {
                return;
            }
            
            _isLoading = value;
            StateHasChanged();
        }
    }

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
        _formDesignJson = await FormService.SerializeFormAsync(_formDefinition, true);
    }

    private Task AddField(FieldType fieldType)
    {
        var field = FieldFactory.CreateField(fieldType);
        field.Label = fieldType.ToString();
        _formDefinition.Fields.Add(field);
        SelectedField = field;
        return UpdateFormDesignJsonAsync();
    }
    
    private Task RemoveField(Field field)
    {
        _formDefinition.Fields.Remove(field);
        SelectedField = null;
        return UpdateFormDesignJsonAsync();
    }
    
    private Task HandleFieldPropertyChanged(FieldPropertyChangedArgs args)
    {
        if (args is { PropertyName: nameof(Field.Type), NewValue: FieldType fieldType })
        {
            ChangeFieldType(args.Field, fieldType);
        }
        
        return UpdateFormDesignJsonAsync();
    }

    /// <summary>
    /// Changes the field type if the new field type is different from the current one.
    /// Creates a new field with the new type and copies the properties from the old field.
    /// </summary>
    /// <param name="field"></param>
    /// <param name="newType"></param>
    private void ChangeFieldType(Field field, FieldType newType)
    {
        var newField = FieldFactory.CreateField(newType);
        newField.Label = field.Label;
        newField.Placeholder = field.Placeholder;
        newField.Required = field.Required;
        newField.ReadOnly = field.ReadOnly;
        newField.Disabled = field.Disabled;
        
        var index = _formDefinition.Fields.IndexOf(field);
        _formDefinition.Fields[index] = newField; // Replace the old field with the new one, old one will be deleted by GC
        SelectedField = newField;
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
        
        IsLoading = true;
        Result result;
        
        if (string.IsNullOrEmpty(_formDefinition.Id))
        {
            result = await CreateNewFormAsync();
        }
        else
        {
            result = await FormService.UpdateFormAsync(_formDefinition.Id, _formDefinition);
        }
        
        if (result.Success)
        {
            NotificationService.NotifySuccess("Form saved successfully");
        }
        else
        {
            NotificationService.NotifyError(result.Error);
        }
        
        IsLoading = false;
    }

    private Task OpenLoadFormDialogAsync()
    {
        return DialogService.OpenAsync<LoadFormDialog>("Load Form", new Dictionary<string, object>
        {
            { "FormLoaded", EventCallback.Factory.Create<FormLoadedEventArgs>(this, LoadForm) }
        });
    }
    
    private Task LoadForm(FormLoadedEventArgs args)
    {
        _formDefinition = args.FormDefinition;
        return UpdateFormDesignJsonAsync();
    }
    
    /// <summary>
    /// Sends a request to the server to create a new form.
    /// </summary>
    /// <returns>Operation result</returns>
    private async Task<Result> CreateNewFormAsync()
    {
        var createFormResult = await FormService.CreateFormAsync(_formDefinition);

        if (createFormResult is { Success: true, Data: not null })
        {
            _formDefinition.Id = createFormResult.Data.Id;
            await UpdateFormDesignJsonAsync();
        }

        return createFormResult;
    }
}
