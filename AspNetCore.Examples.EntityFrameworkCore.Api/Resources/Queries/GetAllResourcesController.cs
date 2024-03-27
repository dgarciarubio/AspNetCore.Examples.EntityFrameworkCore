using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Queries;

[Tags("Resources")]
public class GetAllResourcesController(AppDbContext dbContext) : ResourcesController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ResourceDto>))]
    public async Task<IActionResult> GetAll()
    {
        var resources = await dbContext.Resources
            .AsNoTracking()
            .ToArrayAsync();
        return Ok(resources.Select(r => r.AsDto()));
    }
}
