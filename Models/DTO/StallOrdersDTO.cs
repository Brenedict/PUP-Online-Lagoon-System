using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class StallOrdersDTO
    {
        public FoodStall stallDetails { get; set; }

        public string vendorFirstName { get; set; }

        public bool isEmptyOrders { get; set; }

        public List<ItemOrder> ordersList { get; set; }

        public Dictionary<string, List<OrderDetails>> orderDetails { get; set; } = new Dictionary<string, List<OrderDetails>>();
    }
}
