using CoffeeShop.Hubs;
using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CoffeeShop.Controllers
{
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IHubContext<CoffeeHub> coffeeHub;

        public CoffeeController(IOrderService orderService, IHubContext<CoffeeHub> coffeeHub)
        {
            this.orderService = orderService;
            this.coffeeHub = coffeeHub;
        }

        public async Task<IActionResult> OrderCoffee([FromBody] Order order)
        {
            await coffeeHub.Clients.All.SendAsync("NewOrder", order);
            var orderId = orderService.NewOrder();
            return Accepted(orderId);
        }
    }
}
