using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;


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
    [Authorize(Roles = "vendor")]
    public class VendorController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly ApplicationDbContext _dbContext;

        private readonly VendorService _service;

        private readonly OrderService _orderService;
        public VendorController(ILogger<AccountController> logger, ApplicationDbContext dbContext, VendorService service, OrderService orderService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _service = service;
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult VendorDashboard()
        {
            bool isForDashboard = true;
            var stallOrdersDTO = _orderService.getStallOrdersDTO(isForDashboard);
            return View(stallOrdersDTO);
        }

        [HttpGet]
        public IActionResult VendorMenu()
        {
            var vendorMenuDTO = _service.GetMenuDTO();
            return View(vendorMenuDTO);
        }

        [HttpGet]
        public IActionResult VendorOrderHistory()
        {
            bool isForDashboard = false;
            var stallOrdersDTO = _orderService.getStallOrdersDTO(isForDashboard);
            return View(stallOrdersDTO);
        }

        [HttpGet]
        public IActionResult VendorProfile()
        {
            var vendorProfileDTO = _service.GetVendorProfileDTO();
            return View(vendorProfileDTO);
        }

        [HttpPost]
        public IActionResult NewFoodItem()
        {
            _service.addNewFoodItem();

            return RedirectToAction("VendorMenu", "Vendor");
        }

        [HttpGet]
        public IActionResult GetEditModal(string id)
        {
            Console.WriteLine("FIRE");
            var item = _dbContext.FoodItems.Find(id);
            if (item == null) return NotFound();

            return PartialView("_EditItemModal", item);
        }

        [HttpPost]
        public IActionResult UpdateMenuItem(FoodItem updatedItem)
        {
            _service.saveEditFoodItem(updatedItem);
            return RedirectToAction("VendorMenu");
        }
    }
}
