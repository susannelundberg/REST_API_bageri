using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BageriApi.Controllers;

[Route("api/products")]
[ApiController]
public class SupplierProductsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddSupplierProduct(SupplierProduct supplierProduct)
    {
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
}