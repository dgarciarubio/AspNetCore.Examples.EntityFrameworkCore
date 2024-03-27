using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Resources;

public class ResourceEntityConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.Property(r => r.Id)
            .IsRequired()
            .ValueGeneratedNever();
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired();
    }
}
