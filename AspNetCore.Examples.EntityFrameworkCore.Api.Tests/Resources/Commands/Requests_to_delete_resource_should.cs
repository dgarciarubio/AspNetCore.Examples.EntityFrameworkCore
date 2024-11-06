using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Commands;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_delete_resource_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;
    private readonly AppDbContext _appDbContext = hostFixture.AppDbContext;

    [Fact]
    [ResetDatabase]
    public async Task Delete_an_existing_resource()
    {
        var id = Guid.NewGuid();
        var resource = await _given.AResource(res => res with { Id = id }).InTheDatabase();

        var response = await _server.CreateRequest($"resources/{id}")
            .SendAsync("DELETE");

        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        _appDbContext.Resources.Should().NotContain(r => r.Id == id);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_fail_when_deleting_a_non_existing_resource()
    {
        var id = Guid.NewGuid();

        var response = await _server.CreateRequest($"resources/{id}")
            .SendAsync("DELETE");

        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
    }
}