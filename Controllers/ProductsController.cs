using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BageriApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddProduct(Product product)
    {
        try
        {
            Product item = new()
            {
                Name = product.Name
            };

            context.Products.Add(item);
            await context.SaveChangesAsync();
            return Ok("Produkt tillagd");
        }
        catch
        {
            return StatusCode(500, "Ett fel inträffade");
        }
    }

    [HttpGet()]
    public async Task<ActionResult> ListAllSupplierProducts()
    {
        var item = await context.SupplierProducts
        .Include(sp => sp.Supplier)
        .Include(sp => sp.Product)
        .Select(sp => new
        {
            ProductName = sp.Product.Name,
            SupplierName = sp.Supplier.Name,
            pricePerKg = sp.PricePerKg
        }).ToListAsync(); ;

        return Ok(new { Success = true, StatusCode = 200, Items = item.Count, Data = item });
        
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplierProduct(int id)
    {
        var supplierProduct = await context.SupplierProducts
        .Where(sp => sp.ProductId == id)
        .Include(sp => sp.Product)
        .Include(sp => sp.Supplier)
        .ToListAsync();
        var first = supplierProduct.First();
        var item = new
        {
            productName = first.Product.Name,
            Supplier = supplierProduct.Select(sp => new
            {
                supplierName = sp.Supplier.Name,
                price = sp.PricePerKg,
                articleNumber = sp.ArticleNumber
            })
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = item });
    }
}