using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources;

public class ResourceBuilder(IServiceProvider serviceProvider, Func<ResourceDto, ResourceDto> configureResource)
{
    private readonly ResourceDto _resourceDto = configureResource(new()
    {
        Id = Guid.NewGuid(),
        Name = "TestResource",
    });

    public async Task<ResourceDto> InTheDatabase()
    {
        using var scope = serviceProvider.CreateScope();
        using var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var resource = new Resource(_resourceDto.Id, _resourceDto.Name);
        appDbContext.Resources.Add(resource);
        await appDbContext.SaveChangesAsync();

        return resource.AsDto();
    }
}
