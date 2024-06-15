// LoginModel.cs

using System.ComponentModel.DataAnnotations;

namespace CustomERPSolution_Invoices_Currency.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
