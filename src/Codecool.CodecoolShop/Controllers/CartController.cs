using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        public CartService CartService { get; set; }

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            CartService = new CartService(
                OrderDaoDB.GetInstance()
                );
        }

        //public IActionResult Index()
        //{
        //    var order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
        //    ViewBag.cart = order.Items;
        //    ViewBag.total = order.Items.Sum(lineItem => lineItem.Price*lineItem.Quantity);
        //    return View();
        //}

        public IActionResult Add(int id, string name, decimal price)
        {
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") == null) 
            {
                Order order = new Order();
                order.AddLineItem(name, price);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            }
            else
            {
                Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
                int indexInCart = isInCart(id);
                if (indexInCart != -1) order.Items[indexInCart].Quantity++;
                else order.AddLineItem(name, price);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            }
            return RedirectToAction("Index", "Product");
        }

        private int isInCart(int id)
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            for (int i = 0; i< order.Items.Count; i++)
            {
                if (order.Items[i].Id.Equals(id)) return i;
            }
            return -1;
        }
    }
}
