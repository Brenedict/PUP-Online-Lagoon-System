using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class VendorMenuDTO
    {
        public Vendor vendorDetails { get; set; }

        public FoodStall stallDetails { get; set; }

        public List<FoodItem> foodItems { get; set; }
    }
}
