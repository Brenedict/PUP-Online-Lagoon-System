using PUP_Online_Lagoon_System.Models.Orders;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class CartItem
    {
        public string Food_ID { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public int originalPrice { get; set; }
        public int totalQuantityRemain { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Stall_ID { get; set; }

        public bool Status { get; set; }
    }
}
