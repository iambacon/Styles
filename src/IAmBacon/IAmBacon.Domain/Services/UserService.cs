using IAmBacon.Data.Infrastructure;
using IAmBacon.Domain.Services.Interfaces;

namespace IAmBacon.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Security;
    using Model.Common;
    using Model.Entities;

    /// <summary>
    /// The User service.
    /// </summary>
    public class UserService : MembershipServiceBase<User>, IUserService
    {
        #region Public Methods and Operators

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork) 
            : base(repository, unitOfWork)
        {
        }

        /// <summary>
        /// The create method.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public User Create(User entity)
        {
            // Todo: Should probably return Result rather than user.
            string passwordQuestion = string.Empty;
            string passwordAnswer = string.Empty;
            const bool IsApproved = true;
            MembershipCreateStatus status;
            var providerUserKey = new object(); // todo: Need to decide what this will be.

            MembershipUser membershipUser = this.MembershipProvider.CreateUser(
                entity.Email, 
                entity.Password, 
                entity.Email, 
                passwordQuestion, 
                passwordAnswer, 
                IsApproved, 
                providerUserKey, 
                entity.FirstName, 
                entity.LastName, 
                out status);

            if (membershipUser != null && status == MembershipCreateStatus.Success)
            {
                // todo implement automapper.
                return new User
                    {
                        Id = (int)membershipUser.ProviderUserKey, 
                        Active = membershipUser.IsOnline, 
                        DateCreated = membershipUser.CreationDate, 
                        DateModified = membershipUser.LastActivityDate, 
                        Email = membershipUser.Email, 
                        FirstName = entity.FirstName, 
                        LastName = entity.LastName
                    };
            }

            return entity;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public IResult Delete(User entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TEntity"/>.
        /// </returns>
        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="where">
        /// The where.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public IEnumerable<User> Get(Expression<Func<User, bool>> @where)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<User> GetAll()
        {
            return this.Repository.GetAll();
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        public IResult Save(User entity)
        {
            if (entity.Id == 0)
            {
                this.Repository.Add(entity);
            }
            else
            {
                this.Repository.Update(entity);
            }

            this.UnitOfWork.Commit();

            return new Result(true);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Error code to string.
        /// </summary>
        /// <param name="createStatus">
        /// The create status.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return
                        "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}