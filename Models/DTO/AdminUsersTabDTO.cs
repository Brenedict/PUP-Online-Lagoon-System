using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class AdminUsersTabDTO
    {
        public List<Customer> customerList { get; set; }

        public Dictionary<string, int> totalOrders { get; set; } = new Dictionary<string, int>();
    }
}
