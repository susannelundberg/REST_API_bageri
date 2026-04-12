using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.EntityFrameworkCore;
using BageriApi.DTO;

namespace BageriApi.Controllers;

[Route("api/supplierproducts")]
[ApiController]
public class SupplierProductsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddSupplierProduct(SupplierProduct supplierProduct)
    {
        try
        {
            Supplier supplier = await context.Suppliers.FindAsync(supplierProduct.SupplierId);
            if(supplier is null) return BadRequest("Leverantör existerar inte");

            Product product = await context.Products.FindAsync(supplierProduct.ProductId);
            if(product is null) return BadRequest("Produkten existerar inte");
            
            SupplierProduct item = new()
            {
                ProductId = supplierProduct.ProductId,
                SupplierId = supplierProduct.SupplierId,
                PricePerKg = supplierProduct.PricePerKg,
                ArticleNumber = supplierProduct.ArticleNumber
            };

            context.SupplierProducts.Add(item);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return StatusCode(500, "Ett fel inträffade");
        }
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> AddProductToSupplier(int id, PostSupplierProductModel model)
    {
        try
        {
            var supplier = await context.Suppliers.FindAsync(id);

            Product product = new()
            {
                Name = model.Name
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            SupplierProduct supplierProduct = new()
            {
                ProductId = product.Id,
                SupplierId = id,
                PricePerKg = model.PricePerKg,
                ArticleNumber = model.ArticleNumber
            };

            context.SupplierProducts.Add(supplierProduct);

            await context.SaveChangesAsync();

            return Ok();
        }
        catch
        {
            return StatusCode(500, "Ett fel inträffade");
        }
    }

    [HttpPut("{supplierId}/{productId}")]
    public async Task<ActionResult> UpdatePriceOnSupplierProduct(int supplierId, int productId, PutSupplierProductModel Model)
    {
        try
        {
            var supplierProduct = await context.SupplierProducts
            .FirstOrDefaultAsync(sp => sp.SupplierId == supplierId && sp.ProductId == productId);

            if (supplierProduct is null) return NotFound();

            supplierProduct.PricePerKg = Model.PricePerKg;

            await context.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Ett fel inträffade");
        }
    }
}