using FormBuilder.Models;

namespace FormBuilder.Factories;

/// <summary>
/// Factory class for creating field models.
/// </summary>
public static class FieldFactory
{
    /// <summary>
    /// Creates a new field model based on the provided fieldType.
    /// List of available field types: TextField, NumericIntField, NumericDoubleField, SelectField, DateField.
    /// </summary>
    /// <param name="fieldType">The type of the field to create.</param>
    /// <returns>
    /// A generic instance of the field based on the provided fieldType.
    /// </returns>
    public static Field CreateField(FieldType fieldType)
    {
        return fieldType switch
        {
            FieldType.Text => new TextField(),
            FieldType.NumericInt => new NumericIntField(),
            FieldType.NumericDouble => new NumericDoubleField(),
            FieldType.Select => new SelectField(),
            FieldType.Date => new DateField(),
            _ => new TextField()
        };
    }
}
