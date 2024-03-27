using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Fixtures;

public sealed class HostFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    private GivenFixture? _givenFixture;
    public GivenFixture Given => _givenFixture ??= new GivenFixture(Services);

    private AppDbContextFixture? _appDbContextFixture;
    public AppDbContext AppDbContext => (_appDbContextFixture ??= new AppDbContextFixture(Services)).AppDbContext;


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(builder =>
        {
            builder.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:Default", _msSqlContainer.GetConnectionString() },
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        using var scope = Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        await ResetDatabaseAttribute.InitializeAsync(_msSqlContainer.GetConnectionString());
    }

    public override async ValueTask DisposeAsync()
    {
        _appDbContextFixture?.Dispose();
        await _msSqlContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    Task IAsyncLifetime.DisposeAsync() => DisposeAsync().AsTask();
}
