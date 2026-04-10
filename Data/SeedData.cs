using System.Text.Json;
using BageriApi.Entities;

namespace BageriApi.Data;

public class SeedData
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public async Task SeedProducts(BageriContext context)
    {
        if (context.Products.Any()) return;

        var json = File.ReadAllText("Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }

    public async Task SeedSuppliers(BageriContext context)
    {
        if (context.Suppliers.Any()) return;

        var json = File.ReadAllText("Data/Json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

        if (suppliers is not null && suppliers.Count > 0)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
    }
    public async Task SeedSupplierProducts(BageriContext context)
    {
        if (context.SupplierProducts.Any()) return;

        var json = File.ReadAllText("Data/Json/supplierproducts.json");
        var supplierproducts = JsonSerializer.Deserialize<List<SupplierProduct>>(json, options);

        if (supplierproducts is not null && supplierproducts.Count > 0)
        {
            await context.SupplierProducts.AddRangeAsync(supplierproducts);
            await context.SaveChangesAsync();
        }
    }
}
