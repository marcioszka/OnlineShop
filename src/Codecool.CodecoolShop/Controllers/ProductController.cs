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

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoDB.GetInstance(),
                ProductCategoryDaoDB.GetInstance(),
                SupplierDaoDB.GetInstance());
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

        public IActionResult AddToCart()
        {
            return View();
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
