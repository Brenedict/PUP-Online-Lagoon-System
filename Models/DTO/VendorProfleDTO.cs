using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class VendorProfileDTO
    {
        public User userDetails { get; set; }
        
        public Vendor vendorDetails { get; set; }

        public FoodStall stallDetails { get; set; }

        public int totalOrderCount { get; set; }
    }
}
