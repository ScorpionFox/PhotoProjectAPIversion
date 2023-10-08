using System.ComponentModel.DataAnnotations;

namespace PhotoProjectAPI.Data.ViewModels.Authentication
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please provide Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please provide password")]
        public string Password { get; set; }
    }
}
