using FormBuilder.API.Data;
using FormBuilder.API.Entities;
using FormBuilder.API.Mappers;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Controllers;

[Route("api/forms")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public FormController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets a paged list of forms.
    /// </summary>
    /// <param name="pagedQuery">Paged query</param>
    [HttpGet]
    public async Task<PagedResult<FormDto>> GetFormsPaged([FromQuery] PagedQuery pagedQuery)
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
    
    /// <summary>
    /// Gets a form by its id.
    /// </summary>
    /// <param name="id">Form ID</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<FormDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<FormDto>>> GetFormById(string id)
    {
        var form = await _context.Forms.FindAsync(id);
        
        if (form is null)
        {
            return BadRequest(Result.Fail($"Form with id {id} not found"));
        }
        
        return Result<FormDto>.Succeed(form.ToDto());
    }
    
    /// <summary>
    /// Creates a new form.
    /// </summary>
    /// <param name="formCommand">The form data.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Result<FormDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<FormDto>>> CreateForm(CreateFormCommand formCommand)
    {
        if (string.IsNullOrEmpty(formCommand.FormName))
        {
            return BadRequest(Result.Fail("Form name is required"));
        }
        
        if (string.IsNullOrEmpty(formCommand.FormDesign))
        {
            return BadRequest(Result.Fail("Form design is required"));
        }
        
        var newForm = new Form
        {
            FormName = formCommand.FormName,
            FormDesign = formCommand.FormDesign
        };
        
        _context.Forms.Add(newForm);
        await _context.SaveChangesAsync();
        return Result<FormDto>.Succeed(newForm.ToDto());
    }
    
    /// <summary>
    /// Updates a form by its id.
    /// </summary>
    /// <param name="id">Existing form ID</param>
    /// <param name="formCommand">The form data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> UpdateForm(string id, UpdateFormCommand formCommand)
    {
        var existingForm = await _context.Forms.FindAsync(id);
        
        if (existingForm is null)
        {
            return BadRequest(Result<Form>.Fail($"Form with ID {id} not found"));
        }
        
        if (!string.IsNullOrEmpty(formCommand.FormName) && formCommand.FormName != existingForm.FormName)
        {
            existingForm.FormName = formCommand.FormName;
        }
        
        if (!string.IsNullOrEmpty(formCommand.FormDesign) && formCommand.FormDesign != existingForm.FormDesign)
        {
            existingForm.FormDesign = formCommand.FormDesign;
        }
        
        _context.Update(existingForm);
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
    
    /// <summary>
    /// Deletes a form by its id.
    /// </summary>
    /// <param name="id">Form ID</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<FormDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result>> DeleteForm(string id)
    {
        var existingForm = await _context.Forms.FindAsync(id);
        
        if (existingForm is null)
        {
            return BadRequest(Result.Fail($"Form with ID {id} not found"));
        }
        
        _context.Forms.Remove(existingForm);
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
}
