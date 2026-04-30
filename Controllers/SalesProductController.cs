using BageriApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/salesproduct")]
    [ApiController]
    public class SalesProductController : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult> AddSalesProduct(SalesProduct salesProduct)
        {
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> ListAllSalesProducts(SalesProduct salesProduct)
        {
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> FindSalesProduct(SalesProduct salesProduct)
        {
            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateSalesProductPrice(SalesProduct salesProduct)
        {
            return Ok();
        }
    }
}
