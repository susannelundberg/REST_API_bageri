using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BageriApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BageriApi.Controllers
{
    [Route("api/salesproduct")]
    [ApiController]
    public class SalesProductController(BageriContext context) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult> AddSalesProduct(PostSalesProduct model)
        {
            try
            {
                var salesproduct = new SalesProduct
                {
                    SalesProductName = model.SalesProductName,
                    PricePerPiece = model.PricePerPiece,
                    ProductWeight = model.ProductWeight,
                    QuantityInPackage = model.QuantityInPackage,
                    ManufacturingDate = DateTime.Now.Date,
                    BestBeforeDate = DateTime.Now.Date.AddDays(30)
                };
                

                context.SalesProducts.Add(salesproduct);
                var result = await context.SaveChangesAsync();

                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = result });
            }
            catch
            {
                return StatusCode(500, "Ett fel inträffade");
            }
        }

        [HttpGet()]
        public async Task<ActionResult> ListAllSalesProducts()
        {
            var salesproducts = await context.SalesProducts
            .Select(salesProduct => new
            {
                salesProduct.SalesProductName,
                salesProduct.PricePerPiece,
                salesProduct.ProductWeight,
                salesProduct.QuantityInPackage,
                salesProduct.BestBeforeDate,
                salesProduct.ManufacturingDate
            }).ToListAsync();

            return Ok(new  { Success = true, StatusCode = 200, Items = salesproducts.Count, Data = salesproducts });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindSalesProduct(int id)
        {
            var salesproduct = await context.SalesProducts
            .Where(sp => sp.Id == id)
            .Select(sp => new
            {
                sp.SalesProductName,
                sp.PricePerPiece,
                sp.ProductWeight,
                sp.QuantityInPackage,
                sp.BestBeforeDate,
                sp.ManufacturingDate
            })
            .SingleOrDefaultAsync();
            if (salesproduct is not null)
            {
                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = salesproduct });
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSalesProductPrice(int id, SalesProduct salesProduct)
        {
            try
            {
                var update = await context.SalesProducts
                .FirstOrDefaultAsync(sp => sp.Id == id);

                if (update is null) return NotFound();

                update.PricePerPiece = salesProduct.PricePerPiece;

                await context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Ett fel inträffade");
            }
        }
    }
}
