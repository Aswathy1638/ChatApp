using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class Login
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
