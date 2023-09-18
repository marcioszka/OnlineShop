using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
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
                ViewBag.Count = order.CountItems();
            }
            return View(order);
        }

        public IActionResult QuantityUp(int id)
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            int indexInCart = IndexInCart(id);
            order.Items[indexInCart].Quantity++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public IActionResult QuantityDown(int id)
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            int indexInCart = IndexInCart(id);
            order.Items[indexInCart].Quantity--;
            if (order.Items[indexInCart].Quantity == 0)
            {
                order.Items.RemoveAt(indexInCart);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public IActionResult Price()    //TODO: fix
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            order.Sum = order.CountSum();
            ViewBag.TotalPrice = order.Sum;
            return View("Cart", order);
        }

        public IActionResult Checkout()
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            Checkout checkout = new Checkout();
            checkout.OrderId = order.Id;
            ViewBag.Count = order.CountItems();
            return View(checkout);
        }

        public IActionResult CheckoutForm(Checkout checkout)
        {
            checkout.Status = true;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "checkout", checkout);
            return RedirectToAction("Cart");
        }

        public IActionResult Payment()
        {
            if (SessionHelper.GetObjectFromJson<Checkout>(HttpContext.Session, "checkout") == null) return RedirectToAction("Cart");
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            ViewBag.Count = order.CountItems();
            return View(order);
        }

        public IActionResult Pay(CreditCard data)
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            CartService.SaveOrder(order);
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            return View(order);
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
            for (int i = 0; i < order.Items.Count; i++)
            {
                if (order.Items[i].Id.Equals(id)) return i;
            }
            return -1;
        }
    }
}