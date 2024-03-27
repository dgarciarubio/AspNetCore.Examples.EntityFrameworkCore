using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Commands;

[Tags("Resources")]
public class UpdateResourceController(AppDbContext dbContext) : ResourcesController
{
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, ResourceDto resourceDto)
    {
        var resource = await dbContext.Resources.FindAsync(id);
        if (resource is null) return NotFound();
        resource.Name = resourceDto.Name;
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
