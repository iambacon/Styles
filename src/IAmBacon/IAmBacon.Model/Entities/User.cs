namespace IAmBacon.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    /// <summary>
    /// The User class.
    /// </summary>
    public class User : IUser
    {
        #region Implementation of IUser

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the last lockout date.
        /// </summary>
        public DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        public DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last password change date.
        /// </summary>
        public DateTime LastPasswordChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        [Required]
        public string Salt { get; set; }

        #endregion
    }
}
