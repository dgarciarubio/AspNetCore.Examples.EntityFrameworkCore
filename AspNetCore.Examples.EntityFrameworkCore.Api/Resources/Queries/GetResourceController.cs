using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Queries;

[Tags("Resources")]
public class GetResourceController(AppDbContext dbContext) : ResourcesController
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var resource = await dbContext.Resources
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id);
        if (resource is null) return NotFound();
        return Ok(resource.AsDto());
    }
}
