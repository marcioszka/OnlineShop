using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using System.Configuration;
using System.Dynamic;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }
        
        public CartService CartService { get; set; }

        public Order Order { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoDB.GetInstance(),
                ProductCategoryDaoDB.GetInstance(),
                SupplierDaoDB.GetInstance());
            CartService = new CartService(OrderDaoDB.GetInstance()
                );
            Order = new Order();
        }

        public IActionResult Index()
        {
            dynamic myModel = new ExpandoObject();
            myModel.Products = ProductService.GetAllProducts();
            //myModel.Products = ProductService.GetProductsForCategory(1);
            myModel.Categories = ProductService.GetProductCategories();
            myModel.Suppliers = ProductService.GetSuppliers();
            return View(myModel);
        }

        public IActionResult Category(int id)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Products = ProductService.GetProductsForCategory(id);
            myModel.Categories = ProductService.GetProductCategories();
            myModel.Suppliers = ProductService.GetSuppliers();
            return View("Index", myModel);
        }

        public IActionResult Supplier(int id)
        {
            dynamic myModel = new ExpandoObject();
            myModel.Products = ProductService.GetProductsForSupplier(id);
            myModel.Categories = ProductService.GetProductCategories();
            myModel.Suppliers = ProductService.GetSuppliers();
            return View("Index", myModel);
        }

        public IActionResult AddedToCart(string name, decimal price) //id produktu, z DB zczytac produkt, z tego nazwe i cene
        {
            this.Order.AddLineItem(name, price);
            dynamic myModel = new ExpandoObject();
            myModel.Products = ProductService.GetAllProducts();
            myModel.Categories = ProductService.GetProductCategories();
            myModel.Suppliers = ProductService.GetSuppliers();
            ViewData["ItemsInCart"] = this.Order.Items.Count;
            ViewData["OrderId"] = this.Order.Id;
            ViewData["Name"] = name;
            ViewData["Price"] = price;
            return View("Index", myModel);
        }

        public ActionResult Cart(int orderId)
        {
            dynamic order = new ExpandoObject();
            order.Id = orderId;
            order.Details = this.Order.Items;
            return View(order);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
