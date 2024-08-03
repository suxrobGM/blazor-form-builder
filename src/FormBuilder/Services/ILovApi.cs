using FormBuilder.Shared.Models;

namespace FormBuilder.Services;

/// <summary>
/// Represents the API for interacting with list of values.
/// </summary>
public interface ILovApi
{
    /// <summary>
    /// Gets a paged list of List IDs.
    /// </summary>
    /// <param name="pagedQuery">
    /// Paged query object containing the page number and page size.
    /// </param>
    /// <returns>
    /// PagedResult object containing the list of List IDs if successful, otherwise an error message.
    /// </returns>
    ValueTask<PagedResult<int>> GetListIdPagedAsync(PagedQuery pagedQuery);
    
    /// <summary>
    /// Gets list of values filtered by ListId
    /// </summary>
    /// <param name="listId">List ID</param>
    /// <returns>
    /// Result object containing the list of values if successful, otherwise an error message.
    /// </returns>
    ValueTask<Result<LovDto[]>> GetLovAsync(int listId);
}
