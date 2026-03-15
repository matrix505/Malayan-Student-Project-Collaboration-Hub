using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Please provide valid email address")]
        public string Email { get; set; }
    }
}
