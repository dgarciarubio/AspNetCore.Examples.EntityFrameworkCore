using AspNetCore.Examples.EntityFrameworkCore.Api.Extensions;
using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers().Services
    .AddHttpContextAccessor()
    .AddCustomOpenApi();

builder.Services
    .AddDbContext<AppDbContext>(dbContextBuilder =>
    {
        dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCustomOpenApi(builder.Configuration);
}

app.MapControllers();

if (builder.Configuration.GetValue("Infrastructure:Data:MigrateOnStartup", defaultValue: false))
{
    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

await app.RunAsync();

public partial class Program { }