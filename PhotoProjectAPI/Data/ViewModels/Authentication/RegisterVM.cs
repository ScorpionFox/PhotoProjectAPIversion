using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Data.ViewModels.Authentication
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
