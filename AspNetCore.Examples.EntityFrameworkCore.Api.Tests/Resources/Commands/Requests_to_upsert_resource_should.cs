using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources.Commands;

[Collection(nameof(HostCollectionFixture))]
public class Requests_to_upsert_resource_should(HostFixture hostFixture)
{
    private readonly TestServer _server = hostFixture.Server;
    private readonly GivenFixture _given = hostFixture.Given;
    private readonly AppDbContext _appDbContext = hostFixture.AppDbContext;

    private static ResourceDto ValidRequest(Guid id) => new()
    {
        Id = id,
        Name = "Resource",
    };

    [Fact]
    [ResetDatabase]
    public async Task Create_a_resource()
    {
        var id = Guid.NewGuid();
        var request = ValidRequest(id);

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().HaveStatusCode(HttpStatusCode.Created);
        response.Headers.Location.Should().Be($"{_server.BaseAddress}resources/{id}");
        _appDbContext.Resources.Should().Contain(r => r.AsDto() == request);
    }

    [Fact]
    [ResetDatabase]
    public async Task Update_a_resource()
    {
        var id = Guid.NewGuid();
        var resource = await _given.AResource(res => res with { Id = id }).InTheDatabase();
        var request = ValidRequest(id) with { Name = "Updated Resource" };

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().HaveStatusCode(HttpStatusCode.NoContent);
        _appDbContext.Resources.Should().Contain(r => r.AsDto() == request);
    }

    [Fact]
    [ResetDatabase]
    public async Task Not_accept_a_resource_with_invalid_name()
    {
        var id = Guid.NewGuid();
        var request = ValidRequest(id) with { Name = null! };

        var response = await _server.CreateClient()
            .PutAsJsonAsync($"resources/{id}", request);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        problemDetails.Should().NotBeNull();
        problemDetails!.Errors.Should().ContainKey(nameof(ResourceDto.Name));
    }
}