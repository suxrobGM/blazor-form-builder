using System.Text.Json;
using System.Text.Json.Serialization;
using FormBuilder.Models;

namespace FormBuilder.Utils;

internal class FieldJsonConverter : JsonConverter<Field>
{
    public override Field Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDoc.RootElement;

        if (!rootElement.TryGetProperty("type", out var fieldTypeProperty))
        {
            throw new JsonException("The property 'type' is missing");
        }

        if (!fieldTypeProperty.TryGetInt32(out var fieldType))
        {
            throw new JsonException("The value of the property 'type' is not an integer");
        }
        
        var enumFieldType = Enum.Parse<FieldType>(fieldType.ToString());

        Field? field = enumFieldType switch
        {
            FieldType.Text => JsonSerializer.Deserialize<TextField>(rootElement.GetRawText(), options),
            FieldType.NumericInt => JsonSerializer.Deserialize<NumericIntField>(rootElement.GetRawText(), options),
            FieldType.NumericDouble => JsonSerializer.Deserialize<NumericDoubleField>(rootElement.GetRawText(), options),
            FieldType.Select => JsonSerializer.Deserialize<SelectField>(rootElement.GetRawText(), options),
            FieldType.Date => JsonSerializer.Deserialize<DateField>(rootElement.GetRawText(), options),
            _ => throw new NotSupportedException($"The value of the field type '{enumFieldType}' is not supported"),
        };

        if (field is null)
        {
            throw new JsonException($"Failed to deserialize field of type '{enumFieldType}'");
        }

        return field;
    }

    public override void Write(Utf8JsonWriter writer, Field value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
