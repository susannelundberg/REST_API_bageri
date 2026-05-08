using BageriApi;
using BageriApi.Data;
using BageriApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BageriApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Humanizer;
using System.Xml.Linq;

namespace BageriApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController(BageriContext context) : ControllerBase 
    {
        [HttpPost()]
        public async Task<ActionResult> AddOrder (PostOrderModel model)
        {
            try
            {
                var orderItems = new List<OrderItem>();

                foreach (var x in model.OrderItems)
                {
                    var salesProduct = await context.SalesProducts.FirstOrDefaultAsync(s => s.Id == x.SalesProductId);

                    var orderItem = new OrderItem()
                    {
                        SalesProductId = x.SalesProductId,
                        Quantity = x.Quantity,
                        SubTotal = x.Quantity * salesProduct.PricePerPiece
                    };

                    orderItems.Add(orderItem);
                }

                var totalPrice = orderItems.Sum(x => x.SubTotal);

                var neworder = new Order
                {
                    CustomerId = model.CustomerId,
                    OrderDate = DateTime.Now.Date,
                    OrderNumber = new Random().Next(1000, 9999),
                    OrderItem = orderItems,
                    TotalPrice = totalPrice
                };

                // Total summa

                context.Orders.Add(neworder);
                var result = await context.SaveChangesAsync();

                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet()]
        public async Task<ActionResult> ListAllOrders ()
        {
            var orders = await context.Orders
            .Select(order => new
            {
                order.Customer.StoreName,
                order.OrderDate,
                order.OrderNumber,
                order.TotalPrice,

                Items = order.OrderItem.Select(oi => new
                {
                    oi.SalesProduct.SalesProductName,
                    oi.SalesProduct.PricePerPiece,
                    oi.Quantity,
                    oi.SubTotal
                })
            }).ToListAsync();

            return Ok(new { Success = true, StatusCode = 200, Items = orders.Count, Data = orders });
        }

        [HttpGet("{ordernumber}")]
        public async Task<ActionResult> FindOrderByOrderNumber (int ordernumber)
        {
            var orders = await context.Orders
            .Where(o => o.OrderNumber == ordernumber)
            .Select(o => new
            {
                o.Customer.StoreName,
                o.OrderDate,
                o.OrderNumber,
                o.TotalPrice,

                Items = o.OrderItem.Select(oi => new
                {
                    oi.SalesProduct.SalesProductName,
                    oi.SalesProduct.PricePerPiece,
                    oi.Quantity,
                    oi.SubTotal
                })
            })
            .SingleOrDefaultAsync();
            if (orders is not null)
            {
                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = orders });
            }
            return NotFound();
            // VG: Vilken kund har lagt beställningen
            // VG: Vilka produkter är beställda
        }
        [HttpGet("date={orderdate}")]
        public async Task<ActionResult> FindOrderByOrderDate (DateTime orderdate)
        {
            // var parsedDate = DateTime.Parse(orderdate);

            var orders = await context.Orders
            .Where(o => o.OrderDate == orderdate)
            .Select(o => new
            {
                o.Customer.StoreName
            })
            .SingleOrDefaultAsync();
            if (orders is not null)
            {
                return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = orders });
            }
            return NotFound();
            // VG: Vilken kund har lagt beställningen
            // VG: Vilka produkter är beställda
        }
    }
}
