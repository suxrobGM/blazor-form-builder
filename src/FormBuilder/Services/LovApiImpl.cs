using FormBuilder.Shared.Models;
using FormBuilder.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace FormBuilder.Services;

internal class LovApiImpl : ILovApi
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;
    
    public LovApiImpl(FormBuilderOptions options, IMemoryCache cache)
    {
        if (string.IsNullOrEmpty(options.FormApiHost))
        {
            throw new ArgumentException(nameof(options.FormApiHost));
        }
        
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.FormApiHost)
        };
        
        _cache = cache;
        
        _cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = options.CacheExpiration
        };
    }
    
    public async ValueTask<PagedResult<int>> GetListIdPagedAsync(PagedQuery pagedQuery)
    {
        var cacheKey = $"ListIdPaged_{pagedQuery.Page}_{pagedQuery.PageSize}";
        
        if (!_cache.TryGetValue(cacheKey, out PagedResult<int>? cachedResult))
        {
            var response = await _httpClient.GetAsync($"/api/lov?{pagedQuery.ToQueryString()}");
            cachedResult = await HttpResponseHelpers.HandlePagedResponse<int>(response);
            _cache.Set(cacheKey, cachedResult, _cacheEntryOptions);
        }

        return cachedResult!;
    }
    
    public async ValueTask<Result<LovDto[]>> GetLovAsync(int listId)
    {
        var cacheKey = $"Lov_{listId}";
        
        if (!_cache.TryGetValue(cacheKey, out Result<LovDto[]>? cachedResult))
        {
            var response = await _httpClient.GetAsync($"/api/lov/{listId}");
            cachedResult = await HttpResponseHelpers.HandleResponse<LovDto[]>(response);
            _cache.Set(cacheKey, cachedResult, _cacheEntryOptions);
        }

        return cachedResult!;
    }
}
