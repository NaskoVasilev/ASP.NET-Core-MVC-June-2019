using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Set(string name)
        {
            Cart cart;
            var cartAsString = HttpContext.Session.GetString("Cart");
            if(cartAsString == null)
            {
                cart = new Cart();
            }
            else
            {
                cart = JsonConvert.DeserializeObject<Cart>(cartAsString);
            }
            cart.Products.Add(name);

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return Ok();
        }

        public IActionResult Get()
        {
            var cartAsString = HttpContext.Session.GetString("Cart");
            return this.Content(cartAsString);
        }
    }

    public class Cart
    {
        public Cart()
        {
            this.Products = new List<string>();
        }

        public List<string> Products { get; set; }
    }
}
