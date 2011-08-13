using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bennington.Login.Models
{
    public class LoginForm
    {
        [DisplayName("Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}