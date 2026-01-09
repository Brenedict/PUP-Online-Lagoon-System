using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
//  Accesses all account models
using PUP_Online_Lagoon_System.Models.Account;
// Access to Data to Object (DTO's)
using PUP_Online_Lagoon_System.Models.DTO;
//  Accesses all services
using PUP_Online_Lagoon_System.Service;
using System.Diagnostics;
//  For cookies
using System.Security.Claims;

namespace PUP_Online_Lagoon_System.Controllers
{
    //[Authorize]
    public class VendorController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public VendorController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VendorDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendorMenu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendorOrderHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendorProfile()
        {
            return View();
        }

        
    }
}
