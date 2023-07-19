using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public string Email { get; set; }
    }

}