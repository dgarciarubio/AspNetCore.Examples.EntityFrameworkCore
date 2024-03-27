using Respawn;
using System.Reflection;
using Xunit.Sdk;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Fixtures;

public class ResetDatabaseAttribute : BeforeAfterTestAttribute
{
    private static string? _connectionString;
    private static Respawner? _respawner;

    public static async Task InitializeAsync(string connectionString)
    {
        _connectionString = connectionString;
        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToInclude =
            [
                "Resources",
            ],
        });
    }

    public override void Before(MethodInfo methodUnderTest)
    {
        if (_respawner is not null &&
            _connectionString is not null)
        {
            _respawner.ResetAsync(_connectionString)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
