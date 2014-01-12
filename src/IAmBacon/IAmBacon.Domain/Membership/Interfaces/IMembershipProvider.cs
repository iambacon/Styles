namespace IAmBacon.Domain.Membership.Interfaces
{
    using System.Web.Security;

    /// <summary>
    /// The membership provider interface.
    /// </summary>
    public interface IMembershipProvider
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <param name="isApproved">The is approved.</param>
        /// <param name="providerUserKey">The provider user key.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// The <see cref="MembershipUser"/>.
        /// </returns>
        MembershipUser CreateUser(
            string username, 
            string password, 
            string email, 
            string passwordQuestion, 
            string passwordAnswer, 
            bool isApproved, 
            object providerUserKey, 
            out MembershipCreateStatus status);

        #endregion
    }
}