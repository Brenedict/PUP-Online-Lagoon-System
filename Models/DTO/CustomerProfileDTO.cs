using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class CustomerProfileDTO
    {
        public User userDetails { get; set; }
        
        public Customer customerDetails { get; set; }

        public string passwordChangeStatus { get; set; }
    }
}
