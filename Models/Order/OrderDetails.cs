using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.Orders
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Subtotal { get; set; }

        public string Order_ID { get; set; }

        [ForeignKey("Order_ID")]
        public ItemOrder Order { get; set; }

        public string Food_ID { get; set; }

        [ForeignKey("Food_ID")]
        public FoodItem Food { get; set; }
    }
}
