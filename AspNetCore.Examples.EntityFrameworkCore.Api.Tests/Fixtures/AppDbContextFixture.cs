using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Fixtures;

public sealed class AppDbContextFixture : IDisposable
{
    private readonly IServiceScope _scope;
    private readonly AppDbContext _appDbContext;

    public AppDbContext AppDbContext => _appDbContext;

    public AppDbContextFixture(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
        _appDbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
        _scope.Dispose();
    }
}