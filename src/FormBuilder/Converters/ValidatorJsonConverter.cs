using System.Text.Json;
using System.Text.Json.Serialization;
using FormBuilder.Models;

namespace FormBuilder.Converters;

internal class ValidatorJsonConverter : JsonConverter<Validator>
{
    public override Validator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDoc.RootElement;

        if (!rootElement.TryGetProperty("type", out var validatorTypeProperty))
        {
            throw new JsonException("The property 'type' is missing from the validator");
        }

        if (!validatorTypeProperty.TryGetInt32(out var validatorType))
        {
            throw new JsonException("The value of the property 'type' is not an integer");
        }
        
        var enumValidatorType = Enum.Parse<ValidatorType>(validatorType.ToString());

        Validator? validator = enumValidatorType switch
        {
            ValidatorType.Required => JsonSerializer.Deserialize<RequiredValidator>(rootElement.GetRawText(), options),
            ValidatorType.Length => JsonSerializer.Deserialize<LengthValidator>(rootElement.GetRawText(), options),
            ValidatorType.Email => JsonSerializer.Deserialize<EmailValidator>(rootElement.GetRawText(), options),
            ValidatorType.Range => JsonSerializer.Deserialize<RangeValidator>(rootElement.GetRawText(), options),
            _ => throw new NotSupportedException($"The value of the validator type '{enumValidatorType}' is not supported"),
        };

        if (validator is null)
        {
            throw new JsonException($"Failed to deserialize field of type '{enumValidatorType}'");
        }

        return validator;
    }

    public override void Write(Utf8JsonWriter writer, Validator value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
