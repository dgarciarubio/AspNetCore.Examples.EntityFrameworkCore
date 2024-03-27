using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Commands;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_update_resource_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;
    private readonly AppDbContext _appDbContext = hostFixture.AppDbContext;

    private ResourceDto ValidRequest(Guid id) => new ResourceDto
    {
        Id = id,
        Name = "Updated resource",
    };

    [Fact]
    [ResetDatabase]
    public async Task Update_a_resource()
    {
        var id = Guid.NewGuid();
        var resource = await _given.AResource(res => res with { Id = id }).InTheDatabase();
        var request = ValidRequest(id);

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().BeSuccessful();
        _appDbContext.Resources.Should().Contain(r => r.AsDto() == request);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_update_a_resource_with_an_invalid_request()
    {
        var id = Guid.NewGuid();
        var resource = await _given.AResource(res => res with { Id = id }).InTheDatabase();
        var request = ValidRequest(id) with { Name = null! };

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_update_a_non_existing_resource()
    {
        var id = Guid.NewGuid();
        var request = ValidRequest(id);

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}