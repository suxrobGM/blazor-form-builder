using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FormBuilder.API.Data;
using FormBuilder.API.Entities;
using FormBuilder.API.Mappers;
using FormBuilder.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Services;

public class FormService
{
    private readonly JsonSerializerOptions _jsonSerializerDefaultOptions;
    private readonly ApplicationDbContext _context;

    public FormService(ApplicationDbContext context)
    {
        _context = context;
        _jsonSerializerDefaultOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };
    }
    
    public async Task<PagedResult<FormDto>> GetFormsPagedAsync(PagedQuery pagedQuery)
    {
        var forms = await _context.Forms
            .Skip((pagedQuery.Page - 1) * pagedQuery.PageSize)
            .Take(pagedQuery.PageSize)
            .Select(i => i.ToDto())
            .ToArrayAsync();
        
        var itemsCount = await _context.Forms.CountAsync();
        var pagesCount = (int)Math.Ceiling(itemsCount / (double)pagedQuery.PageSize);
        
        return PagedResult<FormDto>.Succeed(forms, pagedQuery.PageSize, pagesCount);
    }
    
    public async Task<Result<FormDto>> GetFormByIdAsync(string id)
    {
        var form = await _context.Forms.FindAsync(id);
        
        if (form is null)
        {
            return Result<FormDto>.Fail($"Form with id {id} not found");
        }
        
        return Result<FormDto>.Succeed(form.ToDto());
    }
    
    public async Task<Result<FormDto>> CreateFormAsync(CreateFormCommand command)
    {
        if (string.IsNullOrEmpty(command.FormName))
        {
            return Result<FormDto>.Fail("Form name is required");
        }
        
        if (string.IsNullOrEmpty(command.FormDesign))
        {
            return Result<FormDto>.Fail("Form design is required");
        }
        
        var newForm = new Form
        {
            FormName = command.FormName,
            FormDesign = command.FormDesign
        };

        var formScheme = await DeserializeFormSchemeAsync(command.FormDesign);
        
        if (formScheme is null)
        {
            return Result<FormDto>.Fail("Invalid form design");
        }

        formScheme.Id = newForm.Id;
        var validatedFormJson = await SerializeFormSchemeAsync(formScheme);
        newForm.FormDesign = validatedFormJson;
        
        _context.Forms.Add(newForm);
        await _context.SaveChangesAsync();
        return Result<FormDto>.Succeed(newForm.ToDto());
    }
    
    public async Task<Result> UpdateFormAsync(string id, UpdateFormCommand command)
    {
        var form = await _context.Forms.FindAsync(id);
        
        if (form is null)
        {
            return Result.Fail($"Form with id {id} not found");
        }
        
        if (string.IsNullOrEmpty(command.FormName))
        {
            return Result.Fail("Form name is required");
        }
        
        if (string.IsNullOrEmpty(command.FormDesign))
        {
            return Result.Fail("Form design is required");
        }
        
        var formScheme = await DeserializeFormSchemeAsync(command.FormDesign);
        
        if (formScheme is null)
        {
            return Result.Fail("Invalid form design");
        }

        form.FormName = command.FormName;
        form.FormDesign = command.FormDesign;
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
    
    public async Task<Result> DeleteFormAsync(string id)
    {
        var form = await _context.Forms.FindAsync(id);
        
        if (form is null)
        {
            return Result.Fail($"Form with id {id} not found");
        }
        
        _context.Forms.Remove(form);
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
    
    private async Task<string> SerializeFormSchemeAsync(FormScheme formScheme)
    {
        using var ms = new MemoryStream();
        await JsonSerializer.SerializeAsync(ms, formScheme, _jsonSerializerDefaultOptions);
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        return await reader.ReadToEndAsync();
    }
    
    private async Task<FormScheme?> DeserializeFormSchemeAsync(string formDesign)
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(formDesign));
        return await JsonSerializer.DeserializeAsync<FormScheme>(ms, _jsonSerializerDefaultOptions);
    }
    
    #region Internal Types

    /// <summary>
    /// Used to validate form JSON.
    /// </summary>
    private class FormScheme
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public object[] Fields { get; set; } = [];
    }

    #endregion
}
