using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;


namespace PUP_Online_Lagoon_System.Controllers
{
    //  This is a vendor controller dedicated for quick toggles
    [Authorize(Roles = "vendor")]
    public class QuickVendorController : Controller
    {

        private readonly ApplicationDbContext _dbContext;

        public QuickVendorController(ILogger<AccountController> logger, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult ToggleFoodItemAvailability(string foodId)
        {
            Console.WriteLine($"FIRE: {foodId}");

            var existingItem = _dbContext.FoodItems.FirstOrDefault(f => f.Food_ID == foodId);
            
            if (existingItem != null)
            {
                existingItem.Availability = !(existingItem.Availability);
                
                _dbContext.SaveChanges();
            }


            return RedirectToAction("VendorMenu", "Vendor");
        }

        [HttpPost]
        public IActionResult ToggleStallStatus(string stallId)
        {
            var stall = _dbContext.FoodStalls.FirstOrDefault(f => f.Stall_ID == stallId);

            if (stall != null)
            {
                stall.Status = !(stall.Status);

                _dbContext.SaveChanges();
            }


            return RedirectToAction("VendorProfile", "Vendor");
        }

        [HttpPost]
        public IActionResult ChangeOrderStatus(string orderId, string status)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Order_ID == orderId);

            if (order != null)
            {
                order.OrderStatus = status;

                _dbContext.SaveChanges();
            }


            return RedirectToAction("VendorDashboard", "Vendor");
        }
    }
}
