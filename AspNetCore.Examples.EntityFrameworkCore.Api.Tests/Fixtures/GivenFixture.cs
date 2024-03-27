using AspNetCore.Examples.EntityFrameworkCore.Api.Resources;
using AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Resources;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Tests.Fixtures;

public class GivenFixture(IServiceProvider serviceProvider)
{
    public ResourceBuilder AResource(Func<ResourceDto, ResourceDto>? configure = null) 
        => new ResourceBuilder(serviceProvider, configure ?? (res => res));
}