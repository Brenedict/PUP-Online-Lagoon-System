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
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAuthUser _authService;

        public AdminController(ILogger<AccountController> logger, IAuthUser authService )
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminManageOrders()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminManageStalls()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminManageUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StallRegister()
        {
            _authService.GenerateDefaultStall(out StallRegisterDTO dto);
            return View(dto);
        }

        [HttpPost]
        public IActionResult StallRegister(StallRegisterDTO dto)
        {
            _authService.RegisterStall(dto);
            return RedirectToAction("AdminManageStalls");
        }
    }
}
