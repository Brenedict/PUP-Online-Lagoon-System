using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
//  Accesses all account models
using PUP_Online_Lagoon_System.Models.Account;
// Access to Data to Object (DTO's)
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;

//  Accesses all services
using PUP_Online_Lagoon_System.Service;
using System.Diagnostics;
//  For cookies
using System.Security.Claims;
using System.Text.Json;

namespace PUP_Online_Lagoon_System.Controllers
{
    [Authorize(Roles = "customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly CustomerService _service;

        private readonly OrderService _orderService;

        public CustomerController(ILogger<AccountController> logger, CustomerService service, OrderService orderService)
        {
            _logger = logger;
            _service = service;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Cart(string stallId)
        {
            var customerStallCheckoutDTO = _service.getCustomerStallCheckoutDTO(stallId);
            return View(customerStallCheckoutDTO); 
        }

        [HttpGet]
        public IActionResult Checkout(string stallId)
        {
            var customerStallCheckoutDTO = _service.getCustomerStallCheckoutDTO(stallId);
            return View(customerStallCheckoutDTO);
        }

        [HttpGet]
        public IActionResult CheckoutCart(string stallId)
        {
            _orderService.checkoutCart(stallId);
            return RedirectToAction("Dashboard", "Customer");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrderHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OrderSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StallMenu(string stallId)
        {
            var customerStallCheckoutDTO = _service.getCustomerStallCheckoutDTO(stallId);
            return View(customerStallCheckoutDTO);
        }

        [HttpGet]
        public IActionResult Stalls()
        {
            var openedStalls = _service.GetOpenFoodStalls();
            return View(openedStalls);
        }

        private List<CartItem> GetCartFromSession()
        {
            var json = HttpContext.Session.GetString("UserCart");
            return json == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json);
        }

        [HttpPost]
        public IActionResult AddToCart(string foodId, string foodName, double price, string stallId, int quantity)
        {
            _orderService.addToCart(foodId, foodName, price, stallId, quantity);

            return RedirectToAction("StallMenu", new { stallId = stallId });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string foodId, string stallId, string customerId)
        {
            _orderService.deleteCartItem(foodId, customerId);

            return RedirectToAction("Cart", new { stallId = stallId });
        }
    }
}
