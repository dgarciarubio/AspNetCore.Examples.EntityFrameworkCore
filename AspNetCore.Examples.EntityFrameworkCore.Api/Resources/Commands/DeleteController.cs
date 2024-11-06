using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Commands;

[Tags("Resources")]
public class DeleteController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _dbContext.Resources.Where(r => r.Id == id).ExecuteDeleteAsync(HttpContext.RequestAborted);
        return NoContent();
    }
}