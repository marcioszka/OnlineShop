using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        //public CartService CartService { get; set; }

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            //CartService = new CartService(
            //    ProductDaoDB.GetInstance();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
