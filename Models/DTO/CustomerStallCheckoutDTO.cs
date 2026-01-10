using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class CustomerStallCheckoutDTO
    {
        public string customerId { get; set; }
        
        public FoodStall stallDetails { get; set; }

        public List<FoodItem> foodItems { get; set; }

        public ItemOrder order { get; set; }

        public static Dictionary<string, List<CartItem>> staticCart {get; set;} = new Dictionary<string, List<CartItem>>();
    }
}
