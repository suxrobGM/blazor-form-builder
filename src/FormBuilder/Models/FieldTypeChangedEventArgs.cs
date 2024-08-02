namespace FormBuilder.Models;

/// <summary>
/// Event arguments for when a field type changes.
/// </summary>
/// <param name="Field">The field that changed.</param>
/// <param name="NewType">The new field type.</param>
public record FieldTypeChangedEventArgs(
    Field Field,
    FieldType NewType
);
