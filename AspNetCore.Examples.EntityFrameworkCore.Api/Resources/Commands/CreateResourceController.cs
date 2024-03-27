using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Commands;

[Tags("Resources")]
public class CreateResourceController(AppDbContext dbContext) : ResourcesController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(ResourceDto resourceDto)
    {
        var existingResource = await dbContext.Resources.FindAsync(resourceDto.Id);
        if (existingResource is not null) return Conflict();
        var resource = new Resource(resourceDto.Id, resourceDto.Name);
        dbContext.Resources.Add(resource);
        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
