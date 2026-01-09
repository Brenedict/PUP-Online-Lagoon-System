using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
//  Accesses all account models
using PUP_Online_Lagoon_System.Models.Account;
// Access to Data to Object (DTO's)
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

//  Accesses all services
using PUP_Online_Lagoon_System.Service;
using System.Diagnostics;
//  For cookies
using System.Security.Claims;

namespace PUP_Online_Lagoon_System.Controllers
{
    //[Authorize]
    public class CustomerController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly CustomerService _service;

        public CustomerController(ILogger<AccountController> logger, CustomerService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
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

        [HttpPost]
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

    }
}
