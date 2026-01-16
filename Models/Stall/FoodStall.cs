using PUP_Online_Lagoon_System.Models.Account;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.Stall
{
    public class FoodStall
    {
        [Key]
        public string Stall_ID { get; set; }

        [Required]
        public string StallName { get; set; }

        [Required]
        public string StallDescription { get; set; }

        [Required]
        public int PrepTime{ get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
