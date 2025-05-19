using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceApp.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User name")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]

        public string Password { get; set; }
    }
}
