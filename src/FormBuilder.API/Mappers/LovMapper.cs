using FormBuilder.API.Entities;
using FormBuilder.Shared.Models;

namespace FormBuilder.API.Mappers;

public static class LovMapper
{
    public static LovDto ToDto(this Lov entity)
    {
        return new LovDto
        {
            ListId = entity.ListId,
            ListValue = entity.ListValue
        };
    }
    
    public static Lov ToEntity(this LovDto dto)
    {
        return new Lov
        {
            ListId = dto.ListId,
            ListValue = dto.ListValue
        };
    }
}
