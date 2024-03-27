using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Commands;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_create_resource_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;
    private readonly AppDbContext _appDbContext = hostFixture.AppDbContext;

    private ResourceDto ValidRequest => new ResourceDto
    {
        Id = Guid.NewGuid(),
        Name = "Created resource",
    };

    [Fact]
    [ResetDatabase]
    public async Task Create_a_resource()
    {
        var request = ValidRequest;

        var response = await _server.CreateClient()
            .PostAsJsonAsync("resources", request);

        response.Should().BeSuccessful();
        _appDbContext.Resources.Should().Contain(r => r.AsDto() == request);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_create_an_invalid_resource()
    {
        var request = ValidRequest with { Name = null! };

        var response = await _server.CreateClient()
            .PostAsJsonAsync("resources", request);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_create_an_already_existing_resource()
    {
        var resource = await _given.AResource().InTheDatabase();
        var request = resource;

        var response = await _server.CreateClient()
            .PostAsJsonAsync("resources", request);

        response.Should().HaveStatusCode(HttpStatusCode.Conflict);
    }
}