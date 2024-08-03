using System.Net.Http.Json;
using FormBuilder.Models;
using FormBuilder.Shared.Models;
using FormBuilder.Utils;

namespace FormBuilder.Services;

internal class FormApiImpl : IFormApi
{
    private readonly HttpClient _httpClient;
    private readonly IFormSerializer _formSerializer;
    
    public FormApiImpl(FormBuilderOptions options, IFormSerializer formSerializer)
    {
        if (string.IsNullOrEmpty(options.FormApiHost))
        {
            throw new ArgumentException(nameof(options.FormApiHost));
        }
        
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.FormApiHost)
        };
        _formSerializer = formSerializer;
    }
    
    public async Task<Result<FormDto>> GetFormByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/api/forms/{id}");
        return await HttpResponseHelpers.HandleResponse<FormDto>(response);
    }

    public async Task<Result<FormDto>> CreateFormAsync(FormDefinition formDefinition)
    {
        var formDesign = await _formSerializer.SerializeAsync(formDefinition);
        var createFormDto = new CreateFormCommand
        {
            FormName = formDefinition.Name,
            FormDesign = formDesign
        };

        var response = await _httpClient.PostAsJsonAsync("/api/forms", createFormDto);
        return await HttpResponseHelpers.HandleResponse<FormDto>(response);
    }

    public async Task<Result> UpdateFormAsync(string id, FormDefinition formDefinition)
    {
        var formDesign = await _formSerializer.SerializeAsync(formDefinition);
        var updateFormDto = new CreateFormCommand
        {
            FormName = formDefinition.Name,
            FormDesign = formDesign
        };

        var response = await _httpClient.PutAsJsonAsync($"/api/forms/{id}", updateFormDto);
        return await HttpResponseHelpers.HandleResponse(response);
    }

    public async Task<Result> DeleteFormAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"/api/forms/{id}");
        return await HttpResponseHelpers.HandleResponse(response);
    }
}
