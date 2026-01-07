using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.Account
{
    public class User
    {
        [Key]
        public string User_ID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
