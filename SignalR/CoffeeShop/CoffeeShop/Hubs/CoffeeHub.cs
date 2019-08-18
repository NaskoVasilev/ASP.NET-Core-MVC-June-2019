using CoffeeShop.Models;
using CoffeeShop.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CoffeeShop.Hubs
{
    public class CoffeeHub : Hub
    {
        private readonly IOrderService orderService;

        public CoffeeHub(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task GetUpdateForOrder(int orderId)
        {
            CheckResult checkResult;

            do
            {
                checkResult = orderService.GetUpdate(orderId);
                if(checkResult.New)
                {
                    await Clients.All.SendAsync("ReceiveOrderUpdate", checkResult.Update);
                }

            }
            while (!checkResult.Finished);

            await this.Clients.All.SendAsync("Finished");
        }
    }
}
