using System.Net.Http.Json;
using System.Text.Json;
using FormBuilder.Models;
using FormBuilder.Shared.Models;
using FormBuilder.Utils;

namespace FormBuilder.Services;

internal class FormService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerDefaultOptions;
    private readonly JsonSerializerOptions _jsonSerializerIndentedOptions;
    
    public FormService(FormBuilderOptions options)
    {
        if (string.IsNullOrEmpty(options.FormApiUrl))
        {
            throw new ArgumentException(nameof(options.FormApiUrl));
        }
        
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.FormApiUrl)
        };
        
        _jsonSerializerDefaultOptions = new JsonSerializerOptions
        {
            Converters = { new FieldJsonConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        _jsonSerializerIndentedOptions = new JsonSerializerOptions
        {
            Converters = { new FieldJsonConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }
    
    /// <summary>
    /// Deserializes the form design JSON string into a FormDefinition object.
    /// </summary>
    /// <param name="formDesign">
    /// Serialized form design JSON string from FormDefinition object.
    /// </param>
    /// <returns>
    /// FormDefinition object if deserialization is successful, otherwise null.
    /// </returns>
    public FormDefinition? DeserializeFormDesign(string formDesign)
    {
        try
        {
            return JsonSerializer.Deserialize<FormDefinition>(formDesign, _jsonSerializerDefaultOptions);
        }
        catch (JsonException e)
        {
            Console.WriteLine("Failed to deserialize form design. Error: {0}", e);
            return null;
        }
    }
    
    /// <summary>
    /// Serializes the FormDefinition object into a JSON string.
    /// </summary>
    /// <param name="formDefinition">
    /// FormDefinition object to serialize.
    /// </param>
    /// <param name="indented">True to indent the JSON string; otherwise, false.</param>
    /// <returns>Serialized JSON string of the FormDefinition object.</returns>
    public string SerializeFormDesign(FormDefinition formDefinition, bool indented = false)
    {
        return JsonSerializer.Serialize(formDefinition, indented ? _jsonSerializerIndentedOptions : _jsonSerializerDefaultOptions);
    }
    
    /// <summary>
    /// Asynchronously serializes the FormDefinition object into a JSON string.
    /// </summary>
    /// <param name="formDefinition">FormDefinition object to serialize.</param>
    /// <param name="indented">True to indent the JSON string; otherwise, false.</param>
    /// <returns>Serialized JSON string of the FormDefinition object.</returns>
    public async Task<string> SerializeFormDesignAsync(FormDefinition formDefinition, bool indented = false)
    {
        using var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, formDefinition, indented ? _jsonSerializerIndentedOptions : _jsonSerializerDefaultOptions);
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        return await reader.ReadToEndAsync();
    }

    #region Form API Methods

    public async Task<Result<FormDto>> GetFormByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/api/forms/{id}");
        return await HandleResponse<FormDto>(response);
    }
    
    public async Task<Result<FormDto>> CreateFormAsync(FormDefinition formDefinition)
    {
        var formDesign = await SerializeFormDesignAsync(formDefinition);
        var createFormDto = new CreateFormCommand
        {
            FormName = formDefinition.Name,
            FormDesign = formDesign
        };
        
        var response = await _httpClient.PostAsJsonAsync("/api/forms", createFormDto);
        return await HandleResponse<FormDto>(response);
    }
    
    public async Task<Result> UpdateFormAsync(string id, FormDefinition formDefinition)
    {
        var formDesign = await SerializeFormDesignAsync(formDefinition);
        var updateFormDto = new CreateFormCommand
        {
            FormName = formDefinition.Name,
            FormDesign = formDesign
        };
        
        var response = await _httpClient.PutAsJsonAsync($"/api/forms/{id}", updateFormDto);
        return await HandleResponse(response);
    }
    
    public async Task<Result> DeleteFormAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"/api/forms/{id}");
        return await HandleResponse(response);
    }

    #endregion

    #region LOV API Methods
    
    /// <summary>
    /// Gets a paged list of List IDs.
    /// </summary>
    /// <param name="pagedQuery">
    /// Paged query object containing the page number and page size.
    /// </param>
    /// <returns>
    /// PagedResult object containing the list of List IDs if successful, otherwise an error message.
    /// </returns>
    public async Task<PagedResult<int>> GetListIdPagedAsync(PagedQuery pagedQuery)
    {
        var response = await _httpClient.GetAsync($"/api/lov?{pagedQuery.ToQueryString()}");
        return await HandlePagedResponse<int>(response);
    }
    
    /// <summary>
    /// Gets list of values filtered by ListId
    /// </summary>
    /// <param name="listId">List ID</param>
    /// <returns>
    /// Result object containing the list of values if successful, otherwise an error message.
    /// </returns>
    public async Task<Result<LovDto[]>> GetLovAsync(int listId)
    {
        var response = await _httpClient.GetAsync($"/api/lov/{listId}");
        return await HandleResponse<LovDto[]>(response);
    }
    
    #endregion
    
    private async Task<Result> HandleResponse(HttpResponseMessage response)
    {
        var result = await HandleResponse<object>(response);
        return result.Success ? Result.Succeed() : Result.Fail(result.Error!);
    }
    
    private async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Result<T>.Fail($"Failed to call API. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<Result<T>>();
        
        if (result is { Success: false, Error: not null })
        {
            return Result<T>.Fail(result.Error!);
        }
        
        return Result<T>.Succeed(result!.Data!);
    }
    
    private async Task<PagedResult<T>> HandlePagedResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return PagedResult<T>.Fail($"Failed to call API. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
        }
        
        var result = await response.Content.ReadFromJsonAsync<PagedResult<T>>();
        
        if (result is { Success: false, Error: not null })
        {
            return PagedResult<T>.Fail(result.Error!);
        }
        
        return PagedResult<T>.Succeed(result!.Data!, result.PageSize, result.PagesCount);
    }
}
