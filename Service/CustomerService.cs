using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{
    public class CustomerService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _dbContext;

        private readonly VendorService _vendorService;

        public CustomerService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, VendorService vendorService)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _vendorService = vendorService;
        }

        public CustomerStallCheckoutDTO getCustomerStallCheckoutDTO(string stallId)
        {
            string customerId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            var foodItems = _vendorService.getAllFoodItems(stallId);
            var stallDetails = _vendorService.getStallDetails(stallId);

            CustomerStallCheckoutDTO newDTO = new CustomerStallCheckoutDTO
            {
                customerId = customerId,
                foodItems = foodItems,
                stallDetails = stallDetails
            };

            return newDTO;
        }

        public List<FoodStall> GetOpenFoodStalls()
        {
            return _dbContext.FoodStalls
                .Where(s => s.Status == false)
                .ToList();
        }
    }
        

}
