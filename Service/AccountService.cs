using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;
using PUP_Online_Lagoon_System.Models.Stall;

namespace PUP_Online_Lagoon_System.Service
{

    public interface IAuthUser
    {
        User? ValidateUser(string email, string password);
        void RegisterCustomer(CustomerRegisterDTO dto);

        void GenerateDefaultStall(out StallRegisterDTO dto);

        void RegisterStall(StallRegisterDTO dto);
    }

    public interface IGenerateCustomId {
        string GenerateCustomUserId();

        string GenerateCustomCustomerId();

        void CustomerRequiredCustomId(out string userId, out string customerId);

        string GenerateCustomStallId();

        string GenerateCustomVendorId();

        void StallRequiredCustomId(out string vendorId, out string stallId, out string userId);
    }

    public class AuthService : IAuthUser, IGenerateCustomId
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public User? ValidateUser(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            return user;
        }

        public void RegisterCustomer(CustomerRegisterDTO dto)
        {
            //  Generate all necessary custom ID's for account registration (user & customer)
            CustomerRequiredCustomId(out string userId, out string customerId);
            Console.WriteLine(userId);
            Console.WriteLine(customerId);
            var newUser = new User
            {
                User_ID = userId,
                Email = dto.Email,
                Password = dto.Password,
                Role = "customer"
            };

            var newCustomer = new Customer
            {
                Customer_ID = customerId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ContactNum = dto.ContactNum,
                User_ID = newUser.User_ID        //  FK connection
            };

            //  Adding new account registration to DBMS
            _dbContext.Users.Add(newUser);
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
        }

        public void RegisterStall(StallRegisterDTO dto)
        {
            var newUser = new User
            {
                User_ID = dto.User_ID,
                Email = dto.Email,
                Password = dto.Password,
                Role = "vendor"
            };

            var newVendor = new Vendor
            {
                Vendor_ID = dto.Vendor_ID,
                User_ID = dto.User_ID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ContactNum = dto.ContactNum
            };

            var newStall = new FoodStall
            {
                Stall_ID = dto.Stall_ID,
                StallName = dto.StallName,
                StallDescription = dto.StallDescription,
                PrepTime = dto.PrepTime,
                Status = dto.Status
            };

            //  Adding new stall registration to DBMS
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
            CreateStallAndVendor(newVendor, newStall);
        }

        public void CreateStallAndVendor(Vendor vendor, FoodStall stall)
        {
            // 1. Save the Vendor first (This generates the Vendor's ID)
            _dbContext.Vendors.Add(vendor);
            _dbContext.SaveChanges();

            // 3. (Crucial) Link the Stall back to the Vendor object if your model requires it
            vendor.Stall_ID = stall.Stall_ID;

            // 4. Add the Stall and save again
            _dbContext.FoodStalls.Add(stall);
            _dbContext.SaveChanges();
        }

        public void GenerateDefaultStall(out StallRegisterDTO dto)
        {
            StallRequiredCustomId(out string vendorId, out string stallId, out string userId);

            StallRegisterDTO newStall = new();

            string stallNum = stallId.Replace("STALL-", "");

            //  Set all required Custom Id's
            newStall.Vendor_ID = vendorId;
            newStall.Stall_ID = stallId;
            newStall.User_ID = userId;

            // Set other defaults
            newStall.Email = $"vendor{stallNum}@lagoon.com";
            newStall.StallName = $"Stall {stallNum}";

            dto = newStall;
        }

        public void CustomerRequiredCustomId(out string userId, out string customerId)
        {
            userId = GenerateCustomUserId();
            customerId = GenerateCustomCustomerId();
        }

        public void StallRequiredCustomId(out string vendorId, out string stallId, out string userId)
        {
            vendorId = GenerateCustomVendorId();
            stallId = GenerateCustomStallId();
            userId = GenerateCustomUserId();
        }

        public string GenerateCustomUserId()
        {
            string userPrefix = "USER-";

            var lastRecord = _dbContext.Users
            .Where(u => u.User_ID.StartsWith(userPrefix))
            .OrderByDescending(u => u.User_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return userPrefix = $"{userPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.User_ID.Replace(userPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{userPrefix}{newNumber:D3}";
            }

            return $"{userPrefix}001";
        }

        public string GenerateCustomCustomerId()
        {
            string customerPrefix = "CUST-";

            var lastRecord = _dbContext.Customers
            .Where(u => u.Customer_ID.StartsWith(customerPrefix))
            .OrderByDescending(u => u.Customer_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return customerPrefix = $"{customerPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.Customer_ID.Replace(customerPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{customerPrefix}{newNumber:D3}";
            }

            return $"{customerPrefix}001";
        }

        public string GenerateCustomStallId()
        {
            string stallPrefix = "STALL-";

            var lastRecord = _dbContext.FoodStalls
            .Where(u => u.Stall_ID.StartsWith(stallPrefix))
            .OrderByDescending(u => u.Stall_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return stallPrefix = $"{stallPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.Stall_ID.Replace(stallPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{stallPrefix}{newNumber:D3}";
            }

            return $"{stallPrefix}001";
        }

        public string GenerateCustomVendorId()
        {
            string vendorPrefix = "VEND-";

            var lastRecord = _dbContext.Vendors
            .Where(u => u.Vendor_ID.StartsWith(vendorPrefix))
            .OrderByDescending(u => u.Vendor_ID)
            .FirstOrDefault();

            if (lastRecord == null)
            {
                return vendorPrefix = $"{vendorPrefix}001"; // Starts at USER-1
            }

            // Isolate numeric value of User ID
            string numericPart = lastRecord.Vendor_ID.Replace(vendorPrefix, "");


            if (int.TryParse(numericPart, out int lastNumber))
            {
                // 3. Increment and format back to 3 digits (or more)
                int newNumber = lastNumber + 1;
                return $"{vendorPrefix}{newNumber:D3}";
            }

            return $"{vendorPrefix}001";
        }
    }

}
