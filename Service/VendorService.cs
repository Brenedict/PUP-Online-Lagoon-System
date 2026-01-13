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

        public VendorProfileDTO GetVendorProfileDTO(string? passwordChangeStatus)
        {
            string userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value;
            string vendorId = _httpContextAccessor.HttpContext.User.FindFirst("RoleId")?.Value;

            var userDetails = getUserDetails(userId);
            var vendorDetails = getVendorDetails(vendorId);
            var stallDetails = getStallDetails(vendorDetails.Stall_ID);

            int totalOrderCount = getTotalOrderCount(vendorDetails.Stall_ID);

            VendorProfileDTO newDTO = new VendorProfileDTO
            {
                userDetails = userDetails,
                stallDetails = stallDetails,
                vendorDetails = vendorDetails,
                totalOrderCount = totalOrderCount,
                passwordChangeStatus = passwordChangeStatus ?? "none"
            };

            return newDTO;
        }

        public List<FoodItem> getAllFoodItems(string stallId)
        {
            return _dbContext.FoodItems
                .Where(s => s.Stall_ID == stallId)
                .ToList();
        }

        public int getTotalOrderCount(string stallId)
        {
            return _dbContext.Orders
                .Where(o => o.Stall_ID == stallId)
                .ToList().Count();
        }

        public User getUserDetails(string userId)
        {
            return _dbContext.Users.FirstOrDefault(u => u.User_ID == userId);
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

                _dbContext.Update(existingItem);
                _dbContext.SaveChanges();
            }
        }

        public void updateVendorPersonalInfo(string vendorId, string firstName, string lastName, string contactNum)
        {
            var vendorDetails = getVendorDetails(vendorId);

            var existingVendorRecord = _dbContext.Vendors.FirstOrDefault(v => v.Vendor_ID == vendorId);

            if(existingVendorRecord != null)
            {
                existingVendorRecord.FirstName = firstName;
                existingVendorRecord.LastName = lastName;
                existingVendorRecord.ContactNum = contactNum;

                _dbContext.Update(existingVendorRecord);
                _dbContext.SaveChanges();
            }
        }

        public void updateStallDetails(string stallId, string stallName, string stallDescription)
        {
            var stallDetails = getStallDetails(stallId);

            var existingStallRecord = _dbContext.FoodStalls.FirstOrDefault(s => s.Stall_ID == stallId);

            if (existingStallRecord != null)
            {
                existingStallRecord.StallName = stallName;
                existingStallRecord.StallDescription = stallDescription;

                _dbContext.Update(existingStallRecord);
                _dbContext.SaveChanges();
            }
        }

        public bool updateVendorPassword(string vendorId, string currPassword, string newPassword, out string passwordChangeStatus)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value; ;

            var existingUserRecord = _dbContext.Users.FirstOrDefault(u => u.User_ID == userId);

            Console.WriteLine($"Entered Curr: {currPassword}\nNew: {newPassword}");

            if(existingUserRecord == null)
            {
                passwordChangeStatus = "none";
                return false;
            }

            if (existingUserRecord.Password != currPassword)
            {
                passwordChangeStatus = "incorrect";
                return false;
            }

            if (existingUserRecord.Password == newPassword)
            {
                passwordChangeStatus = "repeated";
                return false;
            }


            existingUserRecord.Password = newPassword;

            _dbContext.Update(existingUserRecord);
            _dbContext.SaveChanges();

            passwordChangeStatus = "success";
            return true;

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
