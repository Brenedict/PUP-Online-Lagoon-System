using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{
    public class OrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _dbContext;

        private readonly VendorService _vendorService;

        public OrderService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, VendorService vendorService)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _vendorService = vendorService;
        }

        public void deleteCartItem(string foodId, string customerId)
        {
            if (CustomerStallCheckoutDTO.staticCart.TryGetValue(customerId, out List<CartItem> customerCart))
            {
                customerCart.Remove(customerCart.Find(item => item.Food_ID == foodId));
            }
            else
            {
                return;
            }
        }
        public void addToCart(string foodId, string foodName, double price, string stallId, int quantity)
        {
            string customerId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            if (CustomerStallCheckoutDTO.staticCart.TryGetValue(customerId, out List<CartItem> customerCart))
            {
                var existingItem = customerCart.Find(item => item.Food_ID == foodId);

                if(existingItem != null)
                {
                    existingItem.Quantity += quantity;
                    existingItem.Price = existingItem.Quantity * price;
                } 
                
                else
                {
                    var foodItemDetails = _vendorService.getFoodDetails(foodId);
                    
                    CartItem newItem = new CartItem
                    {
                        Stall_ID = stallId,
                        Food_ID = foodId,
                        Quantity = quantity,
                        Price = price * quantity,
                        FoodName = foodItemDetails.FoodName,
                        FoodDescription = foodItemDetails.FoodDescription,
                        //originalPrice = foodItemDetails.Price,
                        totalQuantityRemain = foodItemDetails.Quantity
                    };

                    customerCart.Add(newItem);
                }
            }

            else
            {
                CustomerStallCheckoutDTO.staticCart.Add(customerId, new List<CartItem>());

                var foodItemDetails = _vendorService.getFoodDetails(foodId);

                CartItem newItem = new CartItem
                {
                    Stall_ID = stallId,
                    Food_ID = foodId,
                    Quantity = quantity,
                    Price = price * quantity,
                    FoodName = foodItemDetails.FoodName,
                    FoodDescription = foodItemDetails.FoodDescription,
                    //originalPrice = foodItemDetails.Price,
                    totalQuantityRemain = foodItemDetails.Quantity
                };

                CustomerStallCheckoutDTO.staticCart.TryGetValue(customerId, out List<CartItem> cart);

                cart.Add(newItem);
            }
        }

        public string GenerateCustomOrderId()
        {
            string orderPrefix = "ORDER-";

            var lastRecord = _dbContext.Orders
            .Where(u => u.Order_ID.StartsWith(orderPrefix))
            .OrderByDescending(u => u.Order_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return orderPrefix = $"{orderPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.Order_ID.Replace(orderPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{orderPrefix}{newNumber:D3}";
            }

            return $"{orderPrefix}001";
        }
    }
        

}
