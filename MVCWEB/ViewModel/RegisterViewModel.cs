using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [MinLength(8)]
        [MaxLength(24)]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [EmailAddress(ErrorMessage = "Email Address is Invalid")] 
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword {  get; set; }
        [Required(ErrorMessage = "Birthdate is Required")]
        public DateOnly Birthdate { get; set; }
        public string? Country { get; set; }

    }
}
