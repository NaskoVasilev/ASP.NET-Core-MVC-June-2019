using System;
using System.Collections.Generic;
using System.Threading;
using CoffeeShop.Models;

namespace CoffeeShop.Services
{
    public class OrderService : IOrderService
    {
        public readonly string[] statuses =
        {
            "Grinding beans",
            "Steaming milk",
            "Quality control",
            "Delivering...",
            "Picked up"
        };
        private readonly Random random;
        private readonly List<int> orders;

        public OrderService()
        {
            random = new Random();
            orders = new List<int>();
        }


        public CheckResult GetUpdate(int id)
        {
            Thread.Sleep(1000);

            int order = this.orders[id - 1];

            if(this.random.Next(0, 4) == 2)
            {
                if(statuses.Length > order)
                {
                    var result = new CheckResult
                    {
                        New = true,
                        Update = statuses[order],
                        Finished = statuses.Length - 1 == order
                    };

                    this.orders[id - 1]++;
                    return result;
                }
            }

            return new CheckResult();
        }

        public int NewOrder()
        {
            this.orders.Add(0);
            return this.orders.Count;
        }
    }
}
