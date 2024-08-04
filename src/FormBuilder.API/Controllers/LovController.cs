using FormBuilder.API.Data;
using FormBuilder.API.Mappers;
using FormBuilder.API.Services;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Controllers;

[Route("api/lov")]
[ApiController]
public class LovController : ControllerBase
{
    private readonly LovService _lovService;
    
    public LovController(LovService lovService)
    {
        _lovService = lovService;
    }
    
    /// <summary>
    /// Gets a paged list of List IDs.
    /// </summary>
    /// <param name="pagedQuery">Paged query</param>
    [HttpGet]
    public async Task<ActionResult<PagedResult<int?>>> GetListIdPaged([FromQuery] PagedQuery pagedQuery)
    {
        var result = await _lovService.GetListIdPaged(pagedQuery);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    /// <summary>
    /// Gets list of values filtered by ListId
    /// </summary>
    [HttpGet("{listId:int}")]
    public async Task<ActionResult<Result<LovDto[]>>> GetLov(int listId)
    {
        var result = await _lovService.GetLovAsync(listId);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    /// <summary>
    /// Batch add list of values
    /// </summary>
    /// <param name="command">The form data.</param>
    [HttpPost]
    public async Task<ActionResult<Result>> AddListOfValues(BatchAddLovCommand command)
    {
        var result = await _lovService.AddListOfValuesAsync(command);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    
    /// <summary>
    /// Batch delete list of values
    /// </summary>
    /// <param name="command">
    /// The command containing the list of ListIds to delete
    /// </param>
    [HttpPost("delete")]
    public async Task<ActionResult<Result>> BatchDeleteListOfValues(BatchDeleteLovCommand command)
    {
        var result = await _lovService.DeleteListOfValuesAsync(command);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
