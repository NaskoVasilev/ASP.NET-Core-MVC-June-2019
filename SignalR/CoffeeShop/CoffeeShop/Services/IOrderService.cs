using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public interface IOrderService
    {
        CheckResult GetUpdate(int id);

        int NewOrder();
    }
}
