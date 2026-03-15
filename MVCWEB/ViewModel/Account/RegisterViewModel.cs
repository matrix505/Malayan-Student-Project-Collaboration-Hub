using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(8)]
        [MaxLength(24)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")] 
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password",ErrorMessage ="Password is not matched")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword {  get; set; }
        [Required(ErrorMessage = "Birthdate is Required")]

        public DateOnly Birthdate { get; set; }

        [Required(ErrorMessage ="Country is required")]
        public string? Country { get; set; }

    }
}
