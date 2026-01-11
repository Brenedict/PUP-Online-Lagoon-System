using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.Orders
{
    public class ItemOrder
    {
        [Key]
        public string Order_ID { get; set; }

        [Required]
        public string OrderDate { get; set; } = "";

        [Required]
        public string OrderStatus { get; set; } = "";

        [Required]
        public double OrderTotal { get; set; } = 0.00;

        [Required]
        public string EstPickupTime { get; set; } = "";

        //  Foreign Keys
        public string? Customer_ID { get; set; }

        [ForeignKey("Customer_ID")]
        public Customer? Customer { get; set; }

        public string? Stall_ID { get; set; }

        [ForeignKey("Stall_ID")]
        public FoodStall? Stall { get; set; }
    }
}
