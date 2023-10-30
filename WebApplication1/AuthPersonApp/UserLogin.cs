using System.ComponentModel.DataAnnotations;

namespace WebApplication1.AuthPersonApp
{
    public class UserLogin
    {
        [Required, MaxLength(20)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
