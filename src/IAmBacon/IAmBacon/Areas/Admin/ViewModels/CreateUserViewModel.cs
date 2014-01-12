namespace IAmBacon.Areas.Admin.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The create user view model.
    /// </summary>
    public class CreateUserViewModel
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        #endregion
    }
}