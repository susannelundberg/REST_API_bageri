using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BageriContext>(options => 
{
    options.UseMySQL(
        builder.Configuration.GetConnectionString("mysqldev"));
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

// using var scope = app.Services.CreateScope();
// var context = scope.ServiceProvider.GetRequiredService<BageriContext>();
// var seed = new SeedData();
// await seed.SeedProducts(context);
// await seed.SeedSuppliers(context);
// await seed.SeedSupplierProducts(context);

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BageriContext>();

context.Database.Migrate();

var seed = new SeedData();
await seed.SeedProducts(context);
await seed.SeedSuppliers(context);
await seed.SeedSupplierProducts(context);

app.Run();