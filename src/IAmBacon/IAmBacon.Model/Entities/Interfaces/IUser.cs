namespace IAmBacon.Model.Entities.Interfaces
{
    using System;

    /// <summary>
    /// The user interface.
    /// </summary>
    public interface IUser : IEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether active.
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets the last lockout date.
        /// </summary>
        DateTime LastLockoutDate { get; set; }

        /// <summary>
        /// Gets or sets the last login date.
        /// </summary>
        DateTime LastLoginDate { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the last password change date.
        /// </summary>
        DateTime LastPasswordChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        string Salt { get; set; }

        #endregion
    }
}