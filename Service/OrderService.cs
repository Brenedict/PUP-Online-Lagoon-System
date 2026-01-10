using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{
    public class OrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _dbContext;

        private readonly VendorService _vendorService;

        private readonly CustomerService _customerService;

        public OrderService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor, VendorService vendorService, CustomerService customerService)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _vendorService = vendorService;
            _customerService = customerService;
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
        public void checkoutCart(string stallId)
        {
            var dto = _customerService.getCustomerStallCheckoutDTO(stallId);

            if (dto == null)
            {
                return;
            }

            string orderId = GenerateCustomOrderId();
            string customerId = dto.customerId;

            var newOrder = new ItemOrder
            {
                Order_ID = orderId,
                Customer_ID = customerId,
                Stall_ID = stallId,
                OrderDate = DateTime.Now.ToString(),
                OrderStatus = "pending",
            };

            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();

            if(CustomerStallCheckoutDTO.staticCart.TryGetValue(customerId, out List<CartItem> cart))
            {
                foreach(var cartItem in cart)
                {
                    var newOrderDetail = new OrderDetails
                    {
                        Order_ID = orderId,
                        Food_ID = cartItem.Food_ID,
                        Quantity = cartItem.Quantity,
                        Subtotal = cartItem.Price
                    };

                    _dbContext.OrderDetails.Add(newOrderDetail);
                    _dbContext.SaveChanges();

                    _customerService.updateFoodItemQuantity(cartItem.Food_ID, cartItem.Quantity);
                }
            }


            CustomerStallCheckoutDTO.staticCart.Remove(customerId);

        }
    }
        
}
