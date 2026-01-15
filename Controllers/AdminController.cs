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
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAuthUser _authService;

        private readonly AdminService _service;

        public AdminController(ILogger<AccountController> logger, IAuthUser authService, AdminService service)
        {
            _logger = logger;
            _authService = authService;
            _service = service;
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminManageOrders()
        {
            var adminOrdersViewDTO = _service.GetOrdersTabDTO();
            return View(adminOrdersViewDTO);
        }

        [HttpGet]
        public IActionResult AdminManageStalls()
        {
            var adminStallsViewDTO = _service.GetStallsTabDTO();
            return View(adminStallsViewDTO);
        }

        [HttpGet]
        public IActionResult AdminManageUsers()
        {
            var adminUsersViewDTO = _service.GetUsersTabDTO();
            return View(adminUsersViewDTO);
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
