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
            return RedirectToAction("OrderSuccess", "Customer");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            bool isForDashboard = true;
            var customerOrdersDTO = _orderService.getCustomerOrdersDTO(isForDashboard);
            return View(customerOrdersDTO);
        }

        [HttpGet]
        public IActionResult OrderHistory()
        {
            bool isForDashboard = false;
            var customerOrdersDTO = _orderService.getCustomerOrdersDTO(isForDashboard);
            return View(customerOrdersDTO);
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

        [HttpPost]
        public IActionResult CancelOrder(string orderId)
        {
            _orderService.cancelOrder(orderId);

            return RedirectToAction("Dashboard", "Customer");
        }
    }
}
