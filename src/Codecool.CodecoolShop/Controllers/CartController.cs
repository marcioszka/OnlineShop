﻿using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Dynamic;

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

        public IActionResult Index()
        {
            return View();
        }
    }
}
