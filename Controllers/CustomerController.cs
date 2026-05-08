using BageriApi.Entities;
using BageriApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BageriApi.Controllers
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
        public async Task<ActionResult> ListAllCustomers ()
        {
            //VG: vilka kunder som köpt vilka produkter (inkludera här eller egen endpoint?)

            var customers = await context.Customers
            .Select(customer => new
            {
                customer.Id,
                customer.StoreName,
                customer.Email,
                customer.PhoneNumber,
                customer.ContactPerson,
                customer.DeliveryAddress,
                customer.InvocieAddress,
                OrderHistory = customer.OrderHistory.Select(o => new
                {
                    o.OrderNumber,
                    o.OrderDate,
                    Order = o.OrderItem.Select(oi => new
                    {
                        oi.SalesProduct.SalesProductName,
                        oi.Quantity
                    }),
                    o.TotalPrice
                })
            }).ToListAsync();

            return Ok(new
            {
                Success = true,
                StatusCode = 200,
                Items = customers.Count,
                Data = customers
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindCustomer (int id)
        {
            //När en kund hämtas ska även beställningshistorik finnas med

            var customer = await context.Customers
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.StoreName,
                c.Email,
                c.PhoneNumber,
                c.ContactPerson,
                c.DeliveryAddress,
                c.InvocieAddress,
                OrderHistory = c.OrderHistory.Select(o => new
                {
                    o.OrderNumber,
                    o.OrderDate,
                    Order = o.OrderItem.Select(oi => new
                    {
                        oi.SalesProduct.SalesProductName,
                        oi.Quantity
                    }),
                    o.TotalPrice
                })
            })
            .SingleOrDefaultAsync();
            if (customer is not null)
            {
                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = customer });
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateContactPerson (int id, Customer customer) //Fixa en DTO
        {
            try
            {
                var update = await context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);

                if (update is null) return NotFound();

                update.ContactPerson = customer.ContactPerson;

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
