using FormBuilder.Models;

namespace FormBuilder.Factories;

/// <summary>
/// Factory class for creating field models.
/// </summary>
public static class FieldFactory
{
    /// <summary>
    /// Creates a new field model based on the provided fieldType.
    /// List of available field types: TextField, NumericIntField, NumericDecimalField, SelectField, DateField.
    /// </summary>
    /// <param name="fieldType">The type of the field to create.</param>
    /// <returns>
    /// A generic instance of the field based on the provided fieldType.
    /// </returns>
    public static Field Create(FieldType fieldType)
    {
        return fieldType switch
        {
            FieldType.Text => new TextField(),
            FieldType.NumericInt => new NumericField<int>(),
            FieldType.NumericDecimal => new NumericField<decimal>(),
            FieldType.Select => new SelectField(),
            FieldType.Date => new DateField(),
            _ => new TextField()
        };
    }
    
    /// <summary>
    /// Creates a new field model based on the provided fieldType.
    /// Copies the properties from the old field to the new field.
    /// </summary>
    /// <param name="newFieldType">The type of the field to create.</param>
    /// <returns>
    /// A generic instance of the field based on the provided fieldType.
    /// </returns>
    public static Field CreateFrom(FieldType newFieldType, Field oldField)
    {
        var newField = Create(newFieldType);
        newField.Label = oldField.Label;
        newField.Placeholder = oldField.Placeholder;
        newField.ReadOnly = oldField.ReadOnly;
        newField.Disabled = oldField.Disabled;
        newField.Hint = oldField.Hint;
        newField.Validators = oldField.Validators;
        return newField;
    }
}
