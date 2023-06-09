using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

       
        public IActionResult Cart()
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") != null)
            {
                order.ItemsCount = order.CountItems();
                ViewData["OrderId"] = order.Id;
                ViewBag.count = order.CountItems();
            }
            return View(order);
        }

        [HttpPost]
        public IActionResult Quantity(int id, int quantity)
        {
            int productQuantity = Convert.ToInt32(quantity);
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            int indexInCart = IndexInCart(id);
            if (quantity == 0) 
            { 
                order.Items.RemoveAt(indexInCart); }
            else 
            { 
                order.Items[indexInCart].Quantity = productQuantity; 
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public IActionResult Price()    //TODO: fix
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            order.Sum = order.CountSum();
            ViewBag.Price = order.Sum;
            return View("Cart", order);
        }

        public IActionResult Add(int id)
        {
            LineItem lineItem = CartService.GetProductDetails(id);
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") == null) 
            {
                Order order = new Order();
                order.AddLineItem(lineItem);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            }
            else
            {
                Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
                int indexInCart = IndexInCart(id);
                if (indexInCart != -1) order.Items[indexInCart].Quantity++;
                else order.AddLineItem(lineItem);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            }
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        private int IndexInCart(int id)
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
