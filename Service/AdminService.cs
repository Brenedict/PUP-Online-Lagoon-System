using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{
    public class AdminService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _dbContext;

        private readonly VendorService _vendorService;

        private readonly OrderService _orderService;

        private readonly CustomerService _customerService;

        public AdminService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext, VendorService vendorService, OrderService orderService, CustomerService customerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _vendorService = vendorService;
            _orderService = orderService;
            _customerService = customerService;
        }

        public AdminUsersTabDTO GetUsersTabDTO()
        {
            var customerList = getCustomerAccounts();

            AdminUsersTabDTO newDTO = new AdminUsersTabDTO
            {
                customerList = customerList
            };

            foreach(var customer in customerList)
            {
                newDTO.totalOrders.Add(customer.Customer_ID, getCustomerOrderCount(customer.Customer_ID));
            }


            return newDTO;
        }

        public AdminStallsTabDTO GetStallsTabDTO()
        {
            var stallList = getStalls();

            AdminStallsTabDTO newDTO = new AdminStallsTabDTO();

            foreach(var stall in stallList)
            {
                List<FoodItem> stallFoodItems = getAllFoodItems(stall.Stall_ID);
                var vendor = _vendorService.getVendorDetails(_vendorService.getVendorId(stall.Stall_ID));
                newDTO.stallsList.Add((stall, vendor));
                newDTO.stallDetails.Add(stall.Stall_ID, stallFoodItems);
            }

            return newDTO;
        }

        public AdminOrdersTabDTO GetOrdersTabDTO()
        {
            var orderList = _orderService.getAllOrdersList();

            AdminOrdersTabDTO newDTO = new AdminOrdersTabDTO();

            foreach (var order in orderList)
            {
                string customerName = _customerService.GetCustomerName(order.Customer_ID);
                string stallName = (order.Stall_ID == null) ? "<deleted>" : _vendorService.getStallDetails(order.Stall_ID).StallName;
                int orderItemCount = _orderService.getOrderDetailsList(order.Order_ID).Count();

                newDTO.orderList.Add((order, customerName, stallName, orderItemCount));
            }

            return newDTO;
        }



        public async Task DeleteStallAndOwner(string stallId)
        {
            var stall = await _dbContext.FoodStalls
                .Include(s => s.Vendor)
                .FirstOrDefaultAsync(x => x.Stall_ID == stallId);

            if (stall == null) return;

            var userId = stall.Vendor?.User_ID;

            _dbContext.FoodStalls.Remove(stall);

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _dbContext.Users.FindAsync(userId);
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                }
            }

            await _dbContext.SaveChangesAsync();
        }


        public List<FoodItem> getAllFoodItems(string stallId)
        {
            return _dbContext.FoodItems
                .Where(s => s.Stall_ID == stallId)
                .ToList();
        }

        public int getCustomerOrderCount(string customerId)
        {
            return _dbContext.Orders
                .Where(o => o.Customer_ID == customerId)
                .ToList().Count();
        }

        public List<Customer> getCustomerAccounts()
        {
            return _dbContext.Customers
                   .Include(c => c.User)
                   .ToList();
        }

        public List<Vendor> getVendors()
        {
            return _dbContext.Vendors.ToList();
        }

        public List<FoodStall> getStalls()
        {
            return _dbContext.FoodStalls.ToList();
        }
    }
        

}
