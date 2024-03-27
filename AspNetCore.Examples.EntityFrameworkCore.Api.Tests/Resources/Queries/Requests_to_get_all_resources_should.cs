using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Queries;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_get_all_resources_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;

    [Fact]
    [ResetDatabase]
    public async Task Return_all_resourcess()
    {
        var resource1 = await _given.AResource(res => res with { Name = "Test 1" }).InTheDatabase();
        var resource2 = await _given.AResource(res => res with { Name = "Test 2" }).InTheDatabase();

        var response = await _server.CreateRequest("resources")
            .GetAsync();

        response.Should().BeSuccessful();
        var resources = await response.Content.ReadFromJsonAsync<ResourceDto[]>();
        resources.Should().BeEquivalentTo(new[] {
            resource1,
            resource2,
        });
    }

    [Fact]
    [ResetDatabase]
    public async Task Return_nothing_if_no_resources()
    {
        var response = await _server.CreateRequest("resources")
            .GetAsync();

        response.Should().BeSuccessful();
        var resources = await response.Content.ReadFromJsonAsync<ResourceDto[]>();
        resources.Should().BeEmpty();
    }
}