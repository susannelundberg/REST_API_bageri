using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BageriApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddProduct(Product product)
    {
        Product item = new()
        {
            Name = product.Name,
            ArticleNumber = product.ArticleNumber
        };

        context.Products.Add(item);
        await context.SaveChangesAsync();
        return Ok();

    }
}