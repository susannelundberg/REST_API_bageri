using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BageriContext>(options => 
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlitedev"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();