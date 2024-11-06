using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Queries;

[Tags("Resources")]
public class GetController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var resource = await _dbContext.Resources
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == id);
        return resource switch
        {
            Resource => Ok(resource.AsDto()),
            null => NotFound(),
        };
    }
}