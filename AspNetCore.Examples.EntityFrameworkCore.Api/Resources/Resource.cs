namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

public class Resource(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; set; } = name;
}