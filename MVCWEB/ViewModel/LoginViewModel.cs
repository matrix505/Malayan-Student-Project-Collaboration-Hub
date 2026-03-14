using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is Required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

    }
}
