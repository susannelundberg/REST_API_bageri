using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BageriApi.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddSupplier(Supplier supplier)
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
}