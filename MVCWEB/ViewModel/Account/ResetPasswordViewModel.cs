using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="New password is required")]
        [DataType(DataType.Password)]   
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
