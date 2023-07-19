using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserLoginResponse
    {
        public string Token { get; set; }
        public UserProfile Profile { get; set; }
    }

    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
