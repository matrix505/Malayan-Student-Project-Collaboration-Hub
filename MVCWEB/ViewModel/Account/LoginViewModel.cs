using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
