using System.ComponentModel.DataAnnotations;

namespace IAmBacon.Admin.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
