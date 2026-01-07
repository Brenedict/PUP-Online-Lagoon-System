using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class CustomerRegisterDTO
    {
        //  For User Class
        public int User_ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        //  For Customer Class
        public int Customer_ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNum { get; set; }

    }
}
