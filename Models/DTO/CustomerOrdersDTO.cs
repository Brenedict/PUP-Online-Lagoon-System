using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class CustomerOrdersDTO
    {
        public string customerId { get; set; }

        public string customerName { get; set; }

        public bool isEmptyOrders { get; set; }

        public List<ItemOrder> ordersList { get; set; }

        public Dictionary<string, string> orderStallNames { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, List<OrderDetails>> orderDetails { get; set; } = new Dictionary<string, List<OrderDetails>>();
    }
}
