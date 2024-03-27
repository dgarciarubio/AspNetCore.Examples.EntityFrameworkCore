using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Queries;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_get_resource_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;

    [Fact]
    [ResetDatabase]
    public async Task Return_an_existing_resource()
    {
        var id = Guid.NewGuid();
        var resource = await _given.AResource(res => res with { Id = id }).InTheDatabase();

        var response = await _server.CreateRequest($"resources/{id}")
            .GetAsync();

        response.Should().BeSuccessful();
        var result = await response.Content.ReadFromJsonAsync<ResourceDto>();
        result.Should().BeEquivalentTo(resource);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_return_a_non_existing_resource()
    {
        var id = Guid.NewGuid();

        var response = await _server.CreateRequest($"resources/{id}")
            .GetAsync();

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }
}