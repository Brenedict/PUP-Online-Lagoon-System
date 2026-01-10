using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{
    public class VendorService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ApplicationDbContext _dbContext;

        public VendorService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public VendorMenuDTO GetMenuDTO()
        {
            string vendorId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;
            string stallId = getStallId(vendorId);
            
            var foodItems = getAllFoodItems(stallId);
            var stallDetails = getStallDetails(stallId);
            var vendorDetails = getVendorDetails(vendorId);

            VendorMenuDTO newDTO = new VendorMenuDTO
            {
                foodItems = foodItems,
                stallDetails = stallDetails,
                vendorDetails = vendorDetails
            };

            return newDTO;
        }

        public List<FoodItem> getAllFoodItems(string stallId)
        {
            return _dbContext.FoodItems
                .Where(s => s.Stall_ID == stallId)
                .ToList();
        }

        public Vendor getVendorDetails(string vendorId)
        {
            return _dbContext.Vendors.FirstOrDefault(v => v.Vendor_ID == vendorId);
        }

        public FoodStall getStallDetails(string stallId)
        {
            return _dbContext.FoodStalls.FirstOrDefault(f => f.Stall_ID == stallId);
        }

        public string getStallId(string vendorId)
        {
            string stallId = _dbContext.FoodStalls.FirstOrDefault(f => f.Vendor_ID == vendorId)?.Stall_ID;
            return stallId;
        }

        public FoodItem getFoodDetails(string foodId)
        {
            return _dbContext.FoodItems.FirstOrDefault(f => f.Food_ID == foodId);
        }

        public void addNewFoodItem()
        {
            var newFoodItem = new FoodItem
            {
                Food_ID = GenerateCustomFoodId(),
                Stall_ID = getStallId(_httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value),
                FoodName = "Food (Default)",
                FoodDescription = "Add Description",
                Price = 12.50,
                Quantity = 10,
                Availability = false
            };

            _dbContext.FoodItems.Add(newFoodItem);
            _dbContext.SaveChanges();
        }

        public void saveEditFoodItem(FoodItem updatedItem)
        {
            var existingItem = _dbContext.FoodItems.FirstOrDefault(f => f.Food_ID == updatedItem.Food_ID);

            if (existingItem != null)
            {
                existingItem.FoodName = updatedItem.FoodName;
                existingItem.FoodDescription = updatedItem.FoodDescription;
                existingItem.Price = updatedItem.Price;
                existingItem.Quantity = updatedItem.Quantity;

                _dbContext.SaveChanges();
            }
        }

        public string GenerateCustomFoodId()
        {
            string foodPrefix = "FOOD-";

            var lastRecord = _dbContext.FoodItems
            .Where(u => u.Food_ID.StartsWith(foodPrefix))
            .OrderByDescending(u => u.Food_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return foodPrefix = $"{foodPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.Food_ID.Replace(foodPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{foodPrefix}{newNumber:D3}";
            }

            return $"{foodPrefix}001";
        }
    }
        

}
