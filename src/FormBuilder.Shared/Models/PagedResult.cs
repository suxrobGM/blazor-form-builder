namespace FormBuilder.Shared.Models;

/// <summary>
/// Represents a paged result. This class is used to return a paged list of items.
/// </summary>
/// <typeparam name="T">The datatype of paged items</typeparam>
public class PagedResult<T> : Result<IEnumerable<T>>
{
    public PagedResult() : this(null, 0, 0)
    {
    }

    public PagedResult(IEnumerable<T>? data, int pageSize, int pagesCount)
    {
        Data = data;
        PageSize = pageSize;
        PagesCount = pagesCount;
    }

    /// <summary>
    /// The number of items per page.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int PagesCount { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="PagedResult{T}"/> with <see cref="Success"/> set to <c>true</c>.
    /// </summary>
    /// <param name="items">The data to be returned in the PagedResult object</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="pagesCount">The total number of pages</param>
    public static PagedResult<T> Succeed(IEnumerable<T>? items, int pageSize, int pagesCount) =>
        new(items, pageSize, pagesCount);

    /// <summary>
    /// Creates a new instance of <see cref="PagedResult{T}"/> with <see cref="Success"/> set to <c>false</c> and an error message.
    /// </summary>
    /// <param name="error">The error message to be returned in the PagedResult object</param>
    public new static PagedResult<T> Fail(string error) =>
        new(null, 0, 0) { Error = error };
}
