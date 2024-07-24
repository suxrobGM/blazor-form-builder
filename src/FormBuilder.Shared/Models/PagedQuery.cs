namespace FormBuilder.Shared.Models;

/// <summary>
/// Represents a query that can be paged.
/// </summary>
public class PagedQuery
{
    public PagedQuery(
        string? orderBy = null,
        int page = 1,
        int pageSize = 10)
    {
        OrderBy = orderBy;
        Page = page;
        PageSize = pageSize;
    }
    
    /// <summary>
    /// The field to order by.
    /// </summary>
    public string? OrderBy { get; set; }
    
    /// <summary>
    /// Non-zero-based page number. Default is 1.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// The number of items per page. Default is 10.
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Converts the PagedQuery object into a dictionary.
    /// </summary>
    /// <returns>A dictionary with keys "page", "pageSize", and optionally "orderBy" if it is not null or empty.</returns>
    public virtual IDictionary<string, string> ToDictionary()
    {
        var queryDict = new Dictionary<string, string>
        {
            {"page", Page.ToString()},
            {"pageSize", PageSize.ToString()}
        };

        if (!string.IsNullOrEmpty(OrderBy))
        {
            queryDict.Add("orderBy", OrderBy);
        }

        return queryDict;
    }
    
    public string ToQueryString()
    {
        return string.Join("&", ToDictionary().Select(i => $"{i.Key}={i.Value}"));
    }
}
