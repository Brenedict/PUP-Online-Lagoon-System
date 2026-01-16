using Microsoft.AspNetCore.Mvc;
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
                        originalPrice = price,
                        FoodName = foodItemDetails.FoodName,
                        FoodDescription = foodItemDetails.FoodDescription,
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
                    originalPrice = price,
                    FoodName = foodItemDetails.FoodName,
                    FoodDescription = foodItemDetails.FoodDescription,
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

            //  Initial Service Fee
            double orderTotal = 5.00;

            foreach(var item in CustomerStallCheckoutDTO.staticCart[dto.customerId])
            {
                orderTotal += item.Price;
            }

            string orderId = GenerateCustomOrderId();
            string customerId = dto.customerId;
            string customerName = _customerService.GetCustomerName(customerId);
            int stallPrepTime = _vendorService.getStallDetails(stallId).PrepTime;

            var newOrder = new ItemOrder
            {
                Order_ID = orderId,
                Customer_ID = customerId,
                Stall_ID = stallId,
                OrderDate = DateTime.Now.ToString(),
                OrderStatus = "pending",
                OrderTotal = orderTotal,
                EstPickupTime = TimeOnly.FromDateTime(DateTime.Now.AddMinutes(stallPrepTime)).ToString(),
                RecipientName = customerName
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
                        Subtotal = cartItem.Price,
                        OriginalPrice = cartItem.originalPrice,
                        FoodName = cartItem.FoodName
                    };

                    _dbContext.OrderDetails.Add(newOrderDetail);
                    _dbContext.SaveChanges();

                    updateFoodItemQuantity(cartItem.Food_ID, cartItem.Quantity);
                }
            }


            CustomerStallCheckoutDTO.staticCart.Remove(customerId);

        }

        public void updateFoodItemQuantity(string foodId, int quantity)
        {
            var existingItem = _dbContext.FoodItems.FirstOrDefault(f => f.Food_ID == foodId);
            existingItem.Quantity -= quantity;

            _dbContext.Update(existingItem);
            _dbContext.SaveChanges();
        }

        public CustomerOrdersDTO getCustomerOrdersDTO(bool isForDashboard)
        {
            
            if(!_httpContextAccessor.HttpContext.User.IsInRole("customer"))
            {
                return new CustomerOrdersDTO();
            }

            CustomerOrdersDTO newDTO = new CustomerOrdersDTO();

            string customerId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            List<ItemOrder> ordersList = (isForDashboard == true) ? getCustomerIncompleteOrdersList(customerId) : getCustomerPastOrdersList(customerId);

            newDTO.customerId = customerId;
            newDTO.customerName = _customerService.GetCustomerName(customerId);
            newDTO.ordersList = ordersList;

            if (ordersList.Count > 0)
            {
                newDTO.isEmptyOrders = false;

                foreach (var order in ordersList)
                {
                    newDTO.orderDetails.TryAdd(order.Order_ID, getOrderDetailsList(order.Order_ID));

                    if(order.Stall_ID == null && !newDTO.orderStallNames.TryGetValue("deleted", out string temp)) {
                        newDTO.orderStallNames.Add("deleted", "deleted");
                    }

                    else if(order.Stall_ID != null && !newDTO.orderStallNames.TryGetValue(order.Stall_ID, out string stallName))
                    {
                        newDTO.orderStallNames.Add(order.Stall_ID, _vendorService.getStallDetails(order.Stall_ID).StallName);
                    }
                }
            } 

            else
            {
                newDTO.isEmptyOrders = true;
            }

            return newDTO;
        }

        public StallOrdersDTO getStallOrdersDTO(bool isForDashboard)
        {

            if (!_httpContextAccessor.HttpContext.User.IsInRole("vendor"))
            {
                return new StallOrdersDTO();
            }

            StallOrdersDTO newDTO = new StallOrdersDTO();

            string vendorId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            string stallId = _vendorService.getStallId(vendorId);
            List<ItemOrder> ordersList = (isForDashboard == true) ? getStallIncompleteOrdersList(stallId) : getStallPastOrdersList(stallId);

            newDTO.stallDetails = _vendorService.getStallDetails(stallId);
            newDTO.vendorFirstName = _vendorService.getVendorDetails(vendorId).FirstName;
            newDTO.ordersList = ordersList;

            if (ordersList.Count > 0)
            {
                newDTO.isEmptyOrders = false;

                foreach (var order in ordersList)
                {
                    newDTO.orderDetails.TryAdd(order.Order_ID, getOrderDetailsList(order.Order_ID));
                }
            }

            else
            {
                newDTO.isEmptyOrders = true;
            }

            return newDTO;
        }

        public void cancelOrder(string orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Order_ID == orderId);

            if (order != null)
            {
                order.OrderStatus = "cancelled";

                _dbContext.SaveChanges();
            }
        }
        public List<ItemOrder> getCustomerPastOrdersList(string customerId)
        {
            return _dbContext.Orders
                .Where(o => o.Customer_ID == customerId && (o.OrderStatus == "completed" || o.OrderStatus == "cancelled"))
                .ToList();
        }
        public List<ItemOrder> getCustomerIncompleteOrdersList(string customerId)
        {
            return _dbContext.Orders
                .Where(o => o.Customer_ID == customerId && o.OrderStatus.ToLower() != "cancelled" && o.OrderStatus.ToLower() != "completed")
                .ToList();
        }

        public List<ItemOrder> getStallPastOrdersList(string stallId)
        {
            return _dbContext.Orders
                .Where(o => o.Stall_ID== stallId && (o.OrderStatus == "completed" || o.OrderStatus == "cancelled"))
                .ToList();
        }

        public List<ItemOrder> getStallIncompleteOrdersList(string stallId)
        {
            return _dbContext.Orders
                .Where(o => o.Stall_ID == stallId && o.OrderStatus.ToLower() != "cancelled" && o.OrderStatus.ToLower() != "completed")
                .ToList();
        }

        public List<ItemOrder> getAllOrdersList()
        {
            return _dbContext.Orders.ToList();
        }


        public List<OrderDetails> getOrderDetailsList(string orderId)
        {
            return _dbContext.OrderDetails
                .Where(o => o.Order_ID == orderId)
                .ToList();
        }
    }
        
}
