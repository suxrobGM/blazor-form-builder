using FormBuilder.API.Data;
using FormBuilder.API.Entities;
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
    [ProducesResponseType(typeof(PagedResult<Form>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFormsPaged([FromQuery] PagedQuery pagedQuery)
    {
        var forms = await _context.Forms
            .Skip((pagedQuery.Page - 1) * pagedQuery.PageSize)
            .Take(pagedQuery.PageSize)
            .ToListAsync();
        
        return Ok(forms);
    }
    
    /// <summary>
    /// Gets a form by its id.
    /// </summary>
    /// <param name="id">Form ID</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<Form>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFormById(string id)
    {
        var form = await _context.Forms.FindAsync(id);
        
        if (form is null)
        {
            return BadRequest(Result.Fail($"Form with id {id} not found"));
        }
        
        return Ok(Result<Form>.Succeed(form));
    }
    
    /// <summary>
    /// Creates a new form.
    /// </summary>
    /// <param name="formDto">The form data.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Result<Form>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateForm(CreateFormDto formDto)
    {
        if (string.IsNullOrEmpty(formDto.FormName))
        {
            return BadRequest(Result.Fail("Form name is required"));
        }
        
        if (string.IsNullOrEmpty(formDto.FormDesign))
        {
            return BadRequest(Result.Fail("Form design is required"));
        }
        
        var newForm = new Form
        {
            FormName = formDto.FormName,
            FormDesign = formDto.FormDesign
        };
        
        _context.Forms.Add(newForm);
        await _context.SaveChangesAsync();
        return Ok(Result<Form>.Succeed(newForm));
    }
    
    /// <summary>
    /// Updates a form by its id.
    /// </summary>
    /// <param name="id">Existing form ID</param>
    /// <param name="formDto">The form data.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateForm(string id, UpdateFormDto formDto)
    {
        var existingForm = await _context.Forms.FindAsync(id);
        
        if (existingForm is null)
        {
            return BadRequest(Result<Form>.Fail($"Form with ID {id} not found"));
        }
        
        if (!string.IsNullOrEmpty(formDto.FormName) && formDto.FormName != existingForm.FormName)
        {
            existingForm.FormName = formDto.FormName;
        }
        
        if (!string.IsNullOrEmpty(formDto.FormDesign) && formDto.FormDesign != existingForm.FormDesign)
        {
            existingForm.FormDesign = formDto.FormDesign;
        }
        
        _context.Update(existingForm);
        await _context.SaveChangesAsync();
        return Ok(Result.Succeed());
    }
}
