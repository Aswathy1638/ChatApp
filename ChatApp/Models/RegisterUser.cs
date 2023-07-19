using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class RegisterUser
    {
        public class UserRegistrationRequest
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }

        public class UserRegistrationResponse
        {
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public class UserRegistrationErrorResponse
        {
            public string Error { get; set; }
        }

    }
}
