using FormBuilder.Utils;

namespace FormBuilder.Models;

/// <summary>
/// Represents a form definition.
/// </summary>
public class FormDefinition
{
    /// <summary>
    /// Unique identifier of the form. Will be generated if not provided.
    /// </summary>
    public string Id { get; set; } = Generator.GenerateShortId("form_");
    
    /// <summary>
    /// List of field definitions in the form.
    /// </summary>
    public List<Field> Fields { get; set; } = [];
}
