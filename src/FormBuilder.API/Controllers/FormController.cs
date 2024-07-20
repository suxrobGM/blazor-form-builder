using FormBuilder.API.Data;
using FormBuilder.API.Entities;
using FormBuilder.API.Models;
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
            return NotFound(Result<Form>.Fail($"Form with id {id} not found"));
        }
        
        return Ok(Result<Form>.Succeed(form));
    }
}
