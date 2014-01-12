namespace IAmBacon.Domain.Services
{
    using System;
    using System.Web.Security;

    using Data.Infrastructure;
    using Membership;

    /// <summary>
    /// Base service class for services requiring the membership provider.
    /// Implements Service Base.
    /// </summary>
    /// <typeparam name="TEntity">
    /// The type of the entity.
    /// </typeparam>
    public class MembershipServiceBase<TEntity>
        where TEntity : class
    {
        #region Fields

        private const string Provider = "BaconMembershipProvider";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipServiceBase{TEntity}"/> class. 
        /// Initializes a new instance of the <see cref="MembershipServiceBase&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        public MembershipServiceBase(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            this.MembershipProvider = (BaconMembershipProvider)Membership.Providers[Provider];

            if (this.MembershipProvider == null)
            {
                throw new NullReferenceException("membershipProvider");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentException("unitOfWork");
            }

            this.Repository = repository;
            this.UnitOfWork = unitOfWork;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the membership provider.
        /// </summary>
        protected BaconMembershipProvider MembershipProvider { get; private set; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected IRepository<TEntity> Repository { get; private set; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; private set; }

        #endregion
    }
}