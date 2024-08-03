using System.Net.Http.Json;
using FormBuilder.Shared.Models;

namespace FormBuilder.Utils;

/// <summary>
/// Helper class for handling HTTP responses.
/// </summary>
public static class HttpResponseHelpers
{
    public static async Task<Result> HandleResponse(HttpResponseMessage response)
    {
        var result = await HandleResponse<object>(response);
        return result.Success ? Result.Succeed() : Result.Fail(result.Error!);
    }

    public static async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Result<T>.Fail(
                $"Failed to call API. Status code: {(int)response.StatusCode}, Reason: {response.ReasonPhrase}");
        }

        var result = await response.Content.ReadFromJsonAsync<Result<T>>();

        if (result is { Success: false, Error: not null })
        {
            return Result<T>.Fail(result.Error!);
        }

        return Result<T>.Succeed(result!.Data!);
    }

    public static async Task<PagedResult<T>> HandlePagedResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return PagedResult<T>.Fail(
                $"Failed to call API. Status code: {(int)response.StatusCode}, Reason: {response.ReasonPhrase}");
        }

        var result = await response.Content.ReadFromJsonAsync<PagedResult<T>>();

        if (result is { Success: false, Error: not null })
        {
            return PagedResult<T>.Fail(result.Error!);
        }

        return PagedResult<T>.Succeed(result!.Data!, result.PageSize, result.PagesCount);
    }
}
