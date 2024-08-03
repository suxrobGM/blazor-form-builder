using FormBuilder.Models;

namespace FormBuilder.Services;

/// <summary>
/// Service for serializing and deserializing form designs.
/// </summary>
public interface IFormSerializer
{
    /// <summary>
    /// Deserializes the form design JSON string into a FormDefinition object.
    /// </summary>
    /// <param name="formDesign">
    /// Serialized form design JSON string from FormDefinition object.
    /// </param>
    /// <returns>
    /// FormDefinition object if deserialization is successful, otherwise null.
    /// </returns>
    FormDefinition? Deserialize(string formDesign);

    /// <summary>
    /// Asynchronously deserializes the form design JSON string into a FormDefinition object.
    /// </summary>
    /// <param name="formDesign">
    /// Serialized form design JSON string from FormDefinition object.
    /// </param>
    /// <returns>
    /// FormDefinition object if deserialization is successful, otherwise null.
    /// </returns>
    Task<FormDefinition?> DeserializeAsync(string formDesign);

    /// <summary>
    /// Serializes the FormDefinition object into a JSON string.
    /// </summary>
    /// <param name="formDefinition">
    /// FormDefinition object to serialize.
    /// </param>
    /// <param name="indented">True to indent the JSON string; otherwise, false.</param>
    /// <returns>Serialized JSON string of the FormDefinition object.</returns>
    string Serialize(FormDefinition formDefinition, bool indented = false);

    /// <summary>
    /// Asynchronously serializes the FormDefinition object into a JSON string.
    /// </summary>
    /// <param name="formDefinition">FormDefinition object to serialize.</param>
    /// <param name="indented">True to indent the JSON string; otherwise, false.</param>
    /// <returns>Serialized JSON string of the FormDefinition object.</returns>
    Task<string> SerializeAsync(FormDefinition formDefinition, bool indented = false);
}
