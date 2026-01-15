using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.Text.Json;

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
                stallDetails = stallDetails,
            };

            return newDTO;
        }

        public CustomerProfileDTO GetCustomerProfileDTO(string? passwordChangeStatus)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
            string customerId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;

            var userDetails = getUserDetails(userId);
            var customerDetails = getCustomerDetails(customerId);

            CustomerProfileDTO newDTO = new CustomerProfileDTO
            {
                userDetails = userDetails,
                customerDetails = customerDetails,
                passwordChangeStatus = passwordChangeStatus ?? "none"
            };

            return newDTO;
        }

        public void updateCustomerPersonalInfo(string customerId, string firstName, string lastName, string contactNum)
        {
            var existingCustomerRecord = _dbContext.Customers.FirstOrDefault(c => c.Customer_ID == customerId);

            if (existingCustomerRecord != null)
            {
                existingCustomerRecord.FirstName = firstName;
                existingCustomerRecord.LastName = lastName;
                existingCustomerRecord.ContactNum = contactNum;

                _dbContext.Update(existingCustomerRecord);
                _dbContext.SaveChanges();
            }
        }

        public List<FoodStall> GetOpenFoodStalls()
        {
            return _dbContext.FoodStalls.ToList();
        }

        public User getUserDetails(string userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.User_ID == userId);
        }

        public Customer getCustomerDetails(string customerId)
        {
            return _dbContext.Customers.FirstOrDefault(c => c.Customer_ID == customerId);
        }

        public string GetCustomerName(string customerId)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.Customer_ID == customerId);

            return $"{existingCustomer.FirstName} {existingCustomer.LastName}";
        }

    }
        

}
