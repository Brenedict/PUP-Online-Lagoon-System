using Microsoft.EntityFrameworkCore;
using PUP_Online_Lagoon_System.Models;
using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.DTO;

namespace PUP_Online_Lagoon_System.Service
{
    public interface IAuthUser
    {
        User? ValidateUser(string email, string password);
        void RegisterUser(CustomerRegisterDTO dto);
    }

    public interface IGenerateCustomId {
        string GenerateCustomUserId();

        string GenerateCustomCustomerId();

        void GenerateCustomId(out string userId, out string customerId);
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

        public void RegisterUser(CustomerRegisterDTO dto)
        {
            //  Generate all necessary custom ID's for account registration (user & customer)
            GenerateCustomId(out string userId, out string customerId);
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

        public void GenerateCustomId(out string userId, out string customerId)
        {
            userId = GenerateCustomUserId();
            customerId = GenerateCustomCustomerId();
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

    }
}
