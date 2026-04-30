using BageriApi;
using BageriApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController(BageriContext context) : ControllerBase
    {
        [HttpPost()]
        public async Task<ActionResult> AddCustomer (Customer customer)
        {
            try
            {
                Customer store = new()
                {
                    StoreName = customer.StoreName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    ContactPerson = customer.ContactPerson,
                    DeliveryAddress = customer.DeliveryAddress,
                    InvocieAddress = customer.InvocieAddress
                };

                context.Customers.Add(store);
                await context.SaveChangesAsync();
                return Ok("Kund tillagd");
            }
            catch
            {
                return StatusCode(500, "Ett fel inträffade");
            }
        }

        [HttpGet()]
        public async Task<ActionResult> ListAllCustomers (Customer customer)
        {
            //VG: vilka kunder som köpt vilka produkter (inkludera här eller egen endpoint?)
            return Ok();
        }

        [HttpGet()]
        public async Task<ActionResult> FindCustomer (Customer customer)
        {
            //När en kund hämtas ska även beställningshistorik finnas med
            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateContactPerson (Customer customer)
        {
            return Ok();
        }
    }
}
