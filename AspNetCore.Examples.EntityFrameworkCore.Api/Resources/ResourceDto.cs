namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

public record class ResourceDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}

public static class ResourceConverter
{
    public static ResourceDto AsDto(this Resource resource)
    {
        return new ResourceDto
        {
            Id = resource.Id,
            Name = resource.Name,
        };
    }
}
