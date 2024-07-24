namespace FormBuilder.API.Entities;

/// <summary>
/// Entity class for list of values.
/// </summary>
public class Lov
{
    public int Id { get; set; }
    public int? ListId { get; set; }
    public string? ListValue { get; set; }
}
