using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PUP_Online_Lagoon_System.Models;


//  Accesses all account models
using PUP_Online_Lagoon_System.Models.Account;
// Access to Data to Object (DTO's)
using PUP_Online_Lagoon_System.Models.DTO;
//  Accesses all services
using PUP_Online_Lagoon_System.Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;


//  For cookies
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PUP_Online_Lagoon_System.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly ApplicationDbContext _dbContext;

        private readonly IAuthUser _authService;

        private readonly AdminService _service;

        public AdminController(ILogger<AccountController> logger, ApplicationDbContext dbContext, IAuthUser authService, AdminService service)
        {
            _logger = logger;
            _dbContext = dbContext;
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

        public async Task<IActionResult> DeleteStallAndOwner(string stallId)
        {
            if (string.IsNullOrEmpty(stallId)) return BadRequest();

            await _service.DeleteStallAndOwner(stallId);

            // Redirect back to the list so the user sees the item is gone
            return RedirectToAction("AdminManageStalls", "Admin");
        }

        //public async Task<IActionResult> DeleteCustomer(string customerId)
        //{
        //    if (string.IsNullOrEmpty(stallId)) return BadRequest();

        //    await _service.DeleteStallAndOwner(stallId);

        //    // Redirect back to the list so the user sees the item is gone
        //    return RedirectToAction("AdminManageStalls", "Admin");
        //}

        //public async Task<string> ForceDeleteJunkStall(string targetStallId)
        //{
        //    try
        //    {
        //        var stall = await _dbContext.FoodStalls
        //            .FirstOrDefaultAsync(s => s.Stall_ID == targetStallId);

        //        if (stall == null)
        //        {
        //            return "Stall not found. It might have been deleted already.";
        //        }

        //        _dbContext.FoodStalls.Remove(stall);

        //        await _dbContext.SaveChangesAsync();

        //        return $"Success: {targetStallId} and its related food items have been wiped.";
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Error: {ex.Message}";
        //    }
        //}
    }
}
