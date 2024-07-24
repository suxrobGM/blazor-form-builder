using FormBuilder.API.Data;
using FormBuilder.API.Mappers;
using FormBuilder.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Controllers;

[Route("api/lov")]
[ApiController]
public class LovController : ControllerBase
{
   private readonly ApplicationDbContext _context;
    
    public LovController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets a paged list of List IDs.
    /// </summary>
    /// <param name="pagedQuery">Paged query</param>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<int?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListIdPaged([FromQuery] PagedQuery pagedQuery)
    {
        var listIds = await _context.LovMaster
            .Select(i => i.ListId)
            .Distinct()
            .Skip((pagedQuery.Page - 1) * pagedQuery.PageSize)
            .Take(pagedQuery.PageSize)
            .ToArrayAsync();
        
        var itemsCount = await _context.LovMaster
            .Select(i => i.ListId)
            .Distinct()
            .CountAsync();
        
        var pagesCount = (int)Math.Ceiling(itemsCount / (double)pagedQuery.PageSize);
        
        return Ok(PagedResult<int?>.Succeed(listIds, pagedQuery.PageSize, pagesCount));
    }
    
    /// <summary>
    /// Gets list of values filtered by ListId
    /// </summary>
    [HttpGet("{listId:int}")]
    [ProducesResponseType(typeof(Result<LovDto[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLov(int listId)
    {
        var lovs = await _context.LovMaster
            .Where(i => i.ListId == listId)
            .Select(i => i.ToDto())
            .ToArrayAsync();
        
        return Ok(Result<LovDto[]>.Succeed(lovs));
    }
    
    /// <summary>
    /// Batch add list of values
    /// </summary>
    /// <param name="batchAddLovCommand">The form data.</param>
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddListOfValues(BatchAddLovCommand batchAddLovCommand)
    {
        var lovs = batchAddLovCommand.Lovs.Select(i => i.ToEntity());
        
        await _context.LovMaster.AddRangeAsync(lovs);
        await _context.SaveChangesAsync();
        return Ok(Result.Succeed());
    }
}
