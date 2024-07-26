using FormBuilder.API.Data;
using FormBuilder.API.Mappers;
using FormBuilder.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.API.Services;

public class LovService
{
    private readonly ApplicationDbContext _context;
    
    public LovService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Gets a paged list of List IDs.
    /// </summary>
    /// <param name="pagedQuery">Paged query</param>
    public async Task<PagedResult<int?>> GetListIdPaged(PagedQuery pagedQuery)
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
        
        return PagedResult<int?>.Succeed(listIds, pagedQuery.PageSize, pagesCount);
    }
    
    /// <summary>
    /// Gets list of values filtered by ListId
    /// </summary>
    public async Task<Result<LovDto[]>> GetLovAsync(int listId)
    {
        var lovs = await _context.LovMaster
            .Where(i => i.ListId == listId)
            .Select(i => i.ToDto())
            .ToArrayAsync();
        
        return Result<LovDto[]>.Succeed(lovs);
    }
    
    /// <summary>
    /// Batch add list of values
    /// </summary>
    /// <param name="command">The form data.</param>
    public async Task<Result> AddListOfValuesAsync(BatchAddLovCommand command)
    {
        var lovs = command.Lovs.Select(i => i.ToEntity());
        
        await _context.LovMaster.AddRangeAsync(lovs);
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
    
    /// <summary>
    /// Batch delete list of values
    /// </summary>
    /// <param name="command">
    /// The command containing the list of ListIds to delete
    /// </param>
    public async Task<Result> DeleteListOfValuesAsync(BatchDeleteLovCommand command)
    {
        var lovs = _context.LovMaster.Where(i => i.ListId != null && command.ListIds.Contains(i.ListId.Value));
        
        _context.LovMaster.RemoveRange(lovs);
        await _context.SaveChangesAsync();
        return Result.Succeed();
    }
}
