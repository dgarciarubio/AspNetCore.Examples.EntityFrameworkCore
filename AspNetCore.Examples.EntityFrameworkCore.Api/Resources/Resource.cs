namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

public class Resource
{
    public Guid Id { get; }
    public string Name { get; set; }

    public Resource(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}