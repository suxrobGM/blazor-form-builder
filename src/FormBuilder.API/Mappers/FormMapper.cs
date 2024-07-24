using FormBuilder.API.Entities;
using FormBuilder.Shared.Models;

namespace FormBuilder.API.Mappers;

public static class FormMapper
{
    public static FormDto ToDto(this Form entity)
    {
        return new FormDto
        {
            Id = entity.Id,
            FormName = entity.FormName,
            FormDesign = entity.FormDesign
        };
    }
}
