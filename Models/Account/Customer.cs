using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.Account
{
    public class Customer
    {
        [Key]
        public string Customer_ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ContactNum { get; set; }

        //  Foreign Keys

        public string User_ID { get; set; }

        [ForeignKey("User_ID")]
        public User User { get; set; }
    }
}
