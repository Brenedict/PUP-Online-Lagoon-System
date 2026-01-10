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

    }
}
