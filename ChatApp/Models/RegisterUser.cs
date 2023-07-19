using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class RegisterUser
    {
       
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
