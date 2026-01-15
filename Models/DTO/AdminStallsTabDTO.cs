using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class AdminStallsTabDTO
    {
        public List<(FoodStall, Vendor)> stallsList { get; set; } = new List<(FoodStall, Vendor)>();

        public Dictionary<string, List<FoodItem>> stallDetails { get; set; } = new Dictionary<string, List<FoodItem>>();
    }
}
