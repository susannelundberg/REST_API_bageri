using BageriApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult> AddOrder (Order order)
        {
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> ListAllOrders (Order order)
        {
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> FindOrderByOrderNumber (Order order)
        {
            // VG: Vilken kund har lagt beställningen
            // VG: Vilka produkter är beställda
            return Ok();
        }
        [HttpGet()]
        public async Task<ActionResult> FindOrderByOrderDate (Order order)
        {
            // VG: Vilken kund har lagt beställningen
            // VG: Vilka produkter är beställda
            return Ok();
        }
    }
}
