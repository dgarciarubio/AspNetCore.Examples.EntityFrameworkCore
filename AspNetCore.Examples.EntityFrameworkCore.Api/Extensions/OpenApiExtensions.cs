using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace AspNetCore.Examples.EntityFrameworkCore.Api.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddCustomOpenApi(this IServiceCollection services)
    {
        return services
            .AddOpenApi(options =>
            {
                options.AddDocumentTransformer(new ServerUrlDocumentTransformer());
            });
    }

    public static WebApplication UseCustomOpenApi(this WebApplication app, IConfiguration configuration)
    {
        app.MapOpenApi();
        app.UseSwaggerUI(configuration.GetSection("OpenApi:SwaggerUI").Bind);
        app.MapScalarApiReference(configuration.GetSection("OpenApi:Scalar").Bind);

        return app;
    }

    internal class ServerUrlDocumentTransformer : IOpenApiDocumentTransformer
    {
        public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var httpContext = context.ApplicationServices
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext ?? throw new InvalidOperationException("Could not obtain the HttpContext instance.");
            foreach (var server in document.Servers.Where(s => s.Url.Contains("[::]")))
            {
                var serverUrl = new Uri(server.Url);
                server.Url = $"{serverUrl.Scheme}://{httpContext.Request.Host}/";
            }
            return Task.CompletedTask;
        }
    }
}
