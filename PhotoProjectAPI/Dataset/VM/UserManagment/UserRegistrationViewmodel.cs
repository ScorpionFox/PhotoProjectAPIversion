using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Dataset.VM.UserManagment
{
    public class UserRegistrationViewmodel
    {
        [Required(ErrorMessage = "Please provide a login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please provide a password")]
        public string Password { get; set; }
    }
}
