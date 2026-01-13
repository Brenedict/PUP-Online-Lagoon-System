using PUP_Online_Lagoon_System.Models.Account;
using PUP_Online_Lagoon_System.Models.Stall;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PUP_Online_Lagoon_System.Models.DTO
{
    public class StallRegisterDTO
    {
        //  For Stall Class
        public string Stall_ID { get; set; }

        public string StallName { get; set; }

        public string StallDescription { get; set; } = "Stall Default Description";

        public int PrepTime { get; set; } = 5;

        public bool Status { get; set; } = false;

        //  For Vendor Class
        public string Vendor_ID { get; set; }

        public string FirstName { get; set; } = "Stall Default Name";

        public string LastName { get; set; } = "Stall Default Name";

        public string ContactNum { get; set; } = "#";

        //  For User Class
        public string User_ID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; } = "vendor123";

        public string Role { get; set; } = "Vendor";
    }
}
