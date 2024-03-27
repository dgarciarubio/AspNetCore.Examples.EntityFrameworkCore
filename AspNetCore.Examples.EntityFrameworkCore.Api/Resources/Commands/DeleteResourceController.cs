using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Commands;

[Tags("Resources")]
public class DeleteResourceController(AppDbContext dbContext) : ResourcesController
{
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var resource = await dbContext.Resources.FindAsync(id);
        if (resource is null) return NotFound();
        dbContext.Resources.Remove(resource);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
