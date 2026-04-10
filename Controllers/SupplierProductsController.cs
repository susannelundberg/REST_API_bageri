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
}