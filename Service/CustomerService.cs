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

        private readonly OrderService _orderService;

        public CustomerService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, VendorService vendorService, OrderService orderService)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _vendorService = vendorService;
            _orderService = orderService;
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

        public List<FoodStall> GetOpenFoodStalls()
        {
            return _dbContext.FoodStalls
                .Where(s => s.Status == false)
                .ToList();
        }

        public void addToCart(int quantity, double price, string foodId, string orderId)
        {
            var existingItem = _dbContext.FoodItems.FirstOrDefault(f => f.Food_ID == foodId);
            existingItem.Quantity -= quantity;

            var existingCartItem = _dbContext.OrderDetails.FirstOrDefault(o => o.Food_ID == foodId && o.status == false && o.Order_ID == orderId);

            var newCartItem = new OrderDetails
            {
                Food_ID = foodId,
                Order_ID = orderId,
                Quantity = quantity,
                Subtotal = quantity * price,
                status = false
            };
            
        }

        public void updateFoodItemQuantity(string foodId, int quantity)
        {
            Console.WriteLine("Reduct {0}", quantity);
        }

    }
        

}
