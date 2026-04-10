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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        try
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
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
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{supplierId}/{productId}")]
    public async Task<ActionResult> UpdateProduct(int supplierId, int productId, PostSupplierProductModel Model)
    {
        try
        {
            var supplierProduct = await context.SupplierProducts
            .Where(sp => sp.SupplierId == supplierId)
            .Where(sp => sp.ProductId == productId)
            .Include(sp => sp.Supplier)
            .Include(sp => sp.Product)
            .FirstOrDefaultAsync();
            
            supplierProduct.PricePerKg = Model.PricePerKg;

            await context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}