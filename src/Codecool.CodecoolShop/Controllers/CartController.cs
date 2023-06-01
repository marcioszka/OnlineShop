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

       
        public IActionResult Cart()
        {
            dynamic cart = new ExpandoObject();
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") == null)
            {
                cart.Message = "There is no item in your shopping cart.";
            }
            else
            {
                Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
                cart.Id = order.Id;
                cart.Items = order.Items;
                cart.Sum = order.Items.Sum(lineItem => lineItem.Price * lineItem.Quantity);
                ViewBag.count = order.Items.Sum(lineItem => lineItem.Quantity);
            }
            return View(cart);
        }

        public IActionResult Quantity(int id)
        {
            //dynamic cart = new ExpandoObject();
            Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
            for (int i = 0; i < order.Items.Count; i++)
            {
                if (order.Items[i].Id.Equals(id)) order.Items[i].Quantity++;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            return RedirectToAction("Cart");
        }

        public IActionResult Price()
        {
            dynamic cart = new ExpandoObject();
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") == null)
            {
                cart.Message = "There is no item in your shopping cart.";
            }
            else
            {
                Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
                //order.Items.ForEach(lineItem => { lineItem.Price = lineItem.Price*0.1 });
                cart.Id = order.Id;
                cart.Items = order.Items;
                cart.Sum = order.Items.Sum(lineItem => lineItem.Price * lineItem.Quantity);
                cart.Price = order.Items.Sum(lineItem => lineItem.Price * lineItem.Quantity);
            }
            return View("Cart", cart); //TODO: ~/cart/price : me no like it
        }

        //public IActionResult AddedToCart(int id, string name, decimal price) //id produktu, z DB zczytac produkt, z tego nazwe i cene
        //{
        //    this.Order.AddLineItem(name, price);
        //    dynamic myModel = new ExpandoObject();
        //    myModel.Products = ProductService.GetAllProducts();
        //    myModel.Categories = ProductService.GetProductCategories();
        //    myModel.Suppliers = ProductService.GetSuppliers();
        //    ViewData["ItemsInCart"] = this.Order.Items.Count;
        //    ViewData["OrderId"] = this.Order.Id;
        //    ViewData["Name"] = name;
        //    ViewData["Price"] = price;
        //    return View("Index", myModel);
        //}

        public IActionResult Add(int id, string name, decimal price)
        {
            LineItem lineItem = CartService.GetProductDetails(id);
            //LineItem item = new LineItem(id, name, price);
            if (SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order") == null) 
            {
                Order order = new Order();
                order.AddLineItem(lineItem);//item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "order", order);
            }
            else
            {
                Order order = SessionHelper.GetObjectFromJson<Order>(HttpContext.Session, "order");
                int indexInCart = isInCart(id);
                if (indexInCart != -1) order.Items[indexInCart].Quantity++;
                else order.AddLineItem(lineItem); //item) ;
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
