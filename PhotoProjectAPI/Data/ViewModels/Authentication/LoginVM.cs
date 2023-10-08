using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Data.ViewModels.Authentication
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Please provide Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide Password")]
        public string Password { get; set; }
    }
}
