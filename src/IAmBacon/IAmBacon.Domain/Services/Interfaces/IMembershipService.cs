namespace IAmBacon.Domain.Services.Interfaces
{
    /// <summary>
    /// The membership service.
    /// Contains membership and authentication methods.
    /// </summary>
    public interface IMembershipService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool ValidateUser(string username, string password);

        #endregion
    }
}