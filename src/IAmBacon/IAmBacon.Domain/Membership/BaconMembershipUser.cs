namespace IAmBacon.Domain.Membership
{
    using System;
    using System.Web.Security;

    /// <summary>
    /// The bacon custom membership user.
    /// </summary>
    public class BaconMembershipUser : MembershipUser
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaconMembershipUser"/> class.
        /// </summary>
        /// <param name="providerName">The provider name.</param>
        /// <param name="username">The username.</param>
        /// <param name="providerUserKey">The provider user key.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="isApproved">The is approved.</param>
        /// <param name="isLockedOut">The is locked out.</param>
        /// <param name="creationDate">The creation date.</param>
        /// <param name="lastLoginDate">The last login date.</param>
        /// <param name="lastActivityDate">The last activity date.</param>
        /// <param name="lastPasswordChangedDate">The last password changed date.</param>
        /// <param name="lastLockedOutDate">The last locked out date.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        public BaconMembershipUser(
            string providerName, 
            string username, 
            object providerUserKey, 
            string email, 
            string passwordQuestion, 
            string comment, 
            bool isApproved, 
            bool isLockedOut, 
            DateTime creationDate, 
            DateTime lastLoginDate, 
            DateTime lastActivityDate, 
            DateTime lastPasswordChangedDate, 
            DateTime lastLockedOutDate, 
            string firstName, 
            string lastName)
            : base(
                providerName, 
                username, 
                providerUserKey, 
                email, 
                passwordQuestion, 
                comment, 
                isApproved, 
                isLockedOut, 
                creationDate, 
                lastLoginDate, 
                lastActivityDate, 
                lastPasswordChangedDate, 
                lastLockedOutDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        #endregion
    }
}