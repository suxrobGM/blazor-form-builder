using FormBuilder.Utils;

namespace FormBuilder.Models;

/// <summary>
/// Represents a form definition.
/// </summary>
public class FormDefinition
{
    /// <summary>
    /// Form name. Will be generated if not provided.
    /// </summary>
    public string Name { get; set; } = Generator.GenerateShortId("form_");
    
    /// <summary>
    /// List of field definitions in the form.
    /// </summary>
    public List<Field> Fields { get; set; } = [];
}
