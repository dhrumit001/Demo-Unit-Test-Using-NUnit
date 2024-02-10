using System.ComponentModel.DataAnnotations;

namespace Demo_Unit_Test_Using_NUnit.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
