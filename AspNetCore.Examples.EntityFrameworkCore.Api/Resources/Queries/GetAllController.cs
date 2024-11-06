using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Queries;

[Tags("Resources")]
public class GetAllController(AppDbContext dbContext) : ControllerBase
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResourceDto[]))]
    public async Task<IActionResult> Get()
    {
        var resources = await _dbContext.Resources
            .AsNoTracking()
            .ToArrayAsync();
        return Ok(resources.Select(r => r.AsDto()));
    }
}