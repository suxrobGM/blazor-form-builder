using FormBuilder.API.Services;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.API.Controllers;

[Route("api/forms")]
[ApiController]
public class FormController : ControllerBase
{
    private readonly FormService _formService;
    
    public FormController(FormService formService)
    {
        _formService = formService;
    }
    
    /// <summary>
    /// Gets a paged list of forms.
    /// </summary>
    /// <param name="pagedQuery">Paged query</param>
    [HttpGet]
    public async Task<ActionResult<PagedResult<FormDto>>> GetFormsPaged([FromQuery] PagedQuery pagedQuery)
    {
        var result = await _formService.GetFormsPagedAsync(pagedQuery);
        return result.Success ? Ok(result) : BadRequest(result);
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
        var result = await _formService.GetFormByIdAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
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
        var result = await _formService.CreateFormAsync(formCommand);
        return result.Success ? Ok(result) : BadRequest(result);
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
        var result = await _formService.UpdateFormAsync(id, formCommand);
        return result.Success ? Ok(result) : BadRequest(result);
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
        var result = await _formService.DeleteFormAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
