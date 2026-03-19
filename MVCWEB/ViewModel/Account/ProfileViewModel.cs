using System.ComponentModel.DataAnnotations;

namespace MVCWEB.ViewModel.Account
{
    public class ProfileViewModel
    {


        public string? FirstName { get; set; }

    
        public string? LastName { get; set; }

  

        public string? Username { get; set; }

     
        public string? Email { get; set; }

        public DateOnly Birthdate { get; set; }

        public string? Country { get; set; }
    }
}
