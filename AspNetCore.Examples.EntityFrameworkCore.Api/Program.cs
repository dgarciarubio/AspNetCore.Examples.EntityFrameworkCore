using AspNetCore.Examples.EntityFrameworkCore.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers().Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SupportNonNullableReferenceTypes();
    });

builder.Services
    .AddDbContext<AppDbContext>(dbContextBuilder =>
    {
        dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

if (builder.Configuration.GetValue("Infrastructure:Data:MigrateOnStartup", defaultValue: false))
{
    using var scope = app.Services.CreateScope();
    using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

await app.RunAsync();

public partial class Program { }