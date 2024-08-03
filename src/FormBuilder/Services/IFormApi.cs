using FormBuilder.Models;
using FormBuilder.Shared.Models;

namespace FormBuilder.Services;

/// <summary>
/// Represents the API for interacting with forms.
/// </summary>
public interface IFormApi
{
    /// <summary>
    /// Retrieves a form by its ID.
    /// </summary>
    /// <param name="id">Form ID</param>
    /// <returns>
    /// Result object containing the form if successful, otherwise an error message.
    /// </returns>
    Task<Result<FormDto>> GetFormByIdAsync(string id);
    
    /// <summary>
    /// Creates a new form.
    /// </summary>
    /// <param name="formDefinition">
    /// Form definition to create a new form.
    /// </param>
    /// <returns>
    /// Result object containing the created form if successful with the form ID, otherwise an error message.
    /// </returns>
    Task<Result<FormDto>> CreateFormAsync(FormDefinition formDefinition);
    
    /// <summary>
    /// Updates an existing form.
    /// </summary>
    /// <param name="id">Form ID to update.</param>
    /// <param name="formDefinition">
    /// Form definition to update the existing form.
    /// </param>
    /// <returns>
    /// Result object indicating the success or failure of the operation.
    /// </returns>
    Task<Result> UpdateFormAsync(string id, FormDefinition formDefinition);
    
    /// <summary>
    /// Deletes a form by its ID.
    /// </summary>
    /// <param name="id">Form ID to delete</param>
    /// <returns>
    /// Result object indicating the success or failure of the operation.
    /// </returns>
    Task<Result> DeleteFormAsync(string id);
}
