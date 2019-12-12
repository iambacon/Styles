using System.ComponentModel.DataAnnotations;

namespace IAmBacon.Admin.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
