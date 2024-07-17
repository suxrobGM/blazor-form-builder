using FormBuilder.Factories;
using FormBuilder.Models;
using Microsoft.AspNetCore.Components;

namespace FormBuilder.Components;

public partial class FormBuilder : ComponentBase
{
    private FormDefinition _formDefinition = new();
    private Field? _selectedField;
    
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
    }
}
