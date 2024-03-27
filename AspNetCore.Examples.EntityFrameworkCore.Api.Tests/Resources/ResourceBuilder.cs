using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources;

public class ResourceBuilder(IServiceProvider serviceProvider, Func<ResourceDto, ResourceDto> configureResource)
{
    private ResourceDto _resourceDto = new ResourceDto
    {
        Id = Guid.NewGuid(),
        Name = "TestResource",
    };

    public async Task<ResourceDto> InTheDatabase()
    {
        using var scope = serviceProvider.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var resourceDto = configureResource(_resourceDto);
        var resource = new Resource(resourceDto.Id, resourceDto.Name);
        appDbContext.Resources.Add(resource);
        await appDbContext.SaveChangesAsync();

        return resource.AsDto();
    }
}
