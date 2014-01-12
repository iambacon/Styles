namespace IAmBacon.Domain.Services
{
    using Data.Infrastructure;
    using Interfaces;
    using Model.Entities;

    /// <summary>
    /// The membership service.
    /// Contains membership and authentication methods.
    /// </summary>
    public class MembershipService : MembershipServiceBase<User>, IMembershipService
    {
        #region Implementation of IMembershipService

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public MembershipService(IRepository<User> repository, IUnitOfWork unitOfWork) 
            : base(repository, unitOfWork)
        {
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ValidateUser(string username, string password)
        {
            return this.MembershipProvider.ValidateUser(username, password);
        }

        #endregion
    }
}
