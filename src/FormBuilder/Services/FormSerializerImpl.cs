using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FormBuilder.Converters;
using FormBuilder.Models;

namespace FormBuilder.Services;

internal class FormSerializerImpl : IFormSerializer
{
    private readonly JsonSerializerOptions _jsonSerializerDefaultOptions;
    private readonly JsonSerializerOptions _jsonSerializerIndentedOptions;

    public FormSerializerImpl()
    {
        _jsonSerializerDefaultOptions = new JsonSerializerOptions
        {
            Converters = { new FieldJsonConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        _jsonSerializerIndentedOptions = new JsonSerializerOptions
        {
            Converters = { new FieldJsonConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            WriteIndented = true
        };
    }
    
    public FormDefinition? Deserialize(string formDesign)
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
    
    public async Task<FormDefinition?> DeserializeAsync(string formDesign)
    {
        try
        {
            using var ms = new MemoryStream(Encoding.UTF8.GetBytes(formDesign));
            return await JsonSerializer.DeserializeAsync<FormDefinition>(ms, _jsonSerializerDefaultOptions);
        }
        catch (JsonException e)
        {
            Console.WriteLine("Failed to deserialize form design. Error: {0}", e);
            return null;
        }
    }
    
    public string Serialize(FormDefinition formDefinition, bool indented = false)
    {
        return JsonSerializer.Serialize(formDefinition,
            indented ? _jsonSerializerIndentedOptions : _jsonSerializerDefaultOptions);
    }
    
    public async Task<string> SerializeAsync(FormDefinition formDefinition, bool indented = false)
    {
        using var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, formDefinition,
            indented ? _jsonSerializerIndentedOptions : _jsonSerializerDefaultOptions);
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        return await reader.ReadToEndAsync();
    }
}
