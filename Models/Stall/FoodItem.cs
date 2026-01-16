using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.Stall
{
    public class FoodItem
    {
        [Key]
        public string Food_ID { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public string FoodDescription { get; set; }

        [Required]
        public double Price{ get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool Availability { get; set; }

        //  Foreign Keys
        public string? Stall_ID;

        [ForeignKey("Stall_ID")]
        public FoodStall? Stall { get; set; }
    }
}
