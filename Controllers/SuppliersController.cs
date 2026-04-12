using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BageriApi.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddSupplier(Supplier supplier)
    {
        try
        {
            Supplier item = new()
            {
                Name = supplier.Name,
                Address = supplier.Address,
                ContactPerson = supplier.ContactPerson,
                Email = supplier.Email,
                Phone = supplier.Phone
            };

            context.Suppliers.Add(item);
            await context.SaveChangesAsync();
            return Ok();
        }
        catch
        {
            return StatusCode(500, "Ett fel inträffade");
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var supplierProduct = await context.SupplierProducts
        .Where(sp => sp.SupplierId == id)
        .Include(sp => sp.Supplier)
        .Include(sp => sp.Product)
        .ToListAsync();

        var first = supplierProduct.First();
        var data = (new
        {
            supplierName = first.Supplier.Name,
            Product = supplierProduct.Select(sp => new
            {
                productName = sp.Product.Name,
                sp.PricePerKg,
                sp.ArticleNumber

            })
        });

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    }
}