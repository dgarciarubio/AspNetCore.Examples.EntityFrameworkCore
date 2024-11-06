using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources.Commands;

[Tags("Resources")]
public class UpsertController(AppDbContext dbContext) : Controller
{
    private readonly AppDbContext _dbContext = dbContext;

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upsert(Guid id, ResourceDto resourceDto)
    {
        var resource = await _dbContext.Resources.FindAsync(id, HttpContext.RequestAborted);
        return resource switch
        {
            (null) => await Insert(),
            _ => await Update(),
        };

        async Task<IActionResult> Insert()
        {
            var resource = new Resource(id, resourceDto.Name);
            _dbContext.Resources.Add(resource);
            await _dbContext.SaveChangesAsync(HttpContext.RequestAborted);
            return CreatedAtAction(
                actionName: nameof(Queries.GetController.Get),
                controllerName: nameof(Queries.GetController).Replace("Controller", ""),
                routeValues: new { id = resource.Id },
                value: null
            );
        }

        async Task<IActionResult> Update()
        {
            resource.Name = resourceDto.Name;
            await _dbContext.SaveChangesAsync(HttpContext.RequestAborted);
            return NoContent();
        }
    }
}