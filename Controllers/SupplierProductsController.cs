using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.EntityFrameworkCore;

namespace BageriApi.Controllers;

[Route("api/supplierproducts")]
[ApiController]
public class SupplierProductsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddSupplierProduct(SupplierProduct supplierProduct)
    {
        Supplier supplier = await context.Suppliers.FindAsync(supplierProduct.SupplierId);
        if(supplier is null) return BadRequest("Leverantör existerar inte");

        Product product = await context.Products.FindAsync(supplierProduct.ProductId);
        if(product is null) return BadRequest("Produkten existerar inte");
        
        SupplierProduct item = new()
        {
            ProductId = supplierProduct.ProductId,
            SupplierId = supplierProduct.SupplierId,
            PricePerKg = supplierProduct.PricePerKg
        };

        context.SupplierProducts.Add(item);
        await context.SaveChangesAsync();
        return Ok();

    }

    [HttpGet()]
    public async Task<ActionResult> ListAllSupplierProducts()
    {
        var item = await context.SupplierProducts
        .Include(c => c.Supplier)
        .Select(supplierProduct => new
        {
            ProductName = supplierProduct.Product.Name,
            SupplierName = supplierProduct.Supplier.Name
        }).ToListAsync();

        return Ok(new {Success = true, StatusCode = 200, Items = item.Count, Data = item});
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var supplierProduct = await context.SupplierProducts
        .Where(sp => sp.ProductId == id)
        .Include(sp => sp.Product)
        .Include(sp => sp.Supplier)
        .ToListAsync();

        var data = supplierProduct.Select(sp => new
        {
            ProductName = sp.Product.Name,
            SupplierName = sp.Supplier.Name,
            Price = sp.PricePerKg
        });

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    }
}