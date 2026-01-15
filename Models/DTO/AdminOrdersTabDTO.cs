using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class AdminOrdersTabDTO
    {
        public List<(ItemOrder, string, string, int)> orderList { get; set; } = new List<(ItemOrder, string, string, int)>();

    }
}
