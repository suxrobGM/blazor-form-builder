using System.Net.Http.Json;
using FormBuilder.Shared.Models;

namespace FormBuilder.Services;

public class FormService
{
    private readonly HttpClient _httpClient;
    
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
    }
    
    public async Task<Result<FormDto>> CreateFormAsync(CreateFormDto createFormDto)
    {
        var response = await _httpClient.PostAsJsonAsync("forms", createFormDto);
        var result = await response.Content.ReadFromJsonAsync<Result<FormDto>>();

        if (result is { Success: true, Data: not null })
        {
            return Result<FormDto>.Succeed(result.Data);
        }

        return Result<FormDto>.Fail(result!.Error!);
    }
    
    public async Task<Result> UpdateFormAsync(string id, UpdateFormDto updateFormDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"forms/{id}", updateFormDto);
        var result = await response.Content.ReadFromJsonAsync<Result>();

        return result is { Success: true } ? Result.Succeed() : Result.Fail(result!.Error!);
    }
}
