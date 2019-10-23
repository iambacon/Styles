using System;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.UserAggregate
{
    /// <summary>
    /// This is the User Entity.
    /// It must hold the Entity's data attributes as well as the behaviour and logic.
    /// This is an Aggregate Root as well because it is this Entity that will added to the DB.
    /// If this Entity had child Entities they would not be Aggregate roots
    /// </summary>
    public class User : Entity, IAggregateRoot, IDeleteable
    {
        private string _bio;
        private string _profileImage;
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateCreated;
        private DateTime _dateModified;

        public bool IsActive { get; private set; }

        public bool Deleted { get; private set; }

        #region Temporary

        public DateTime LastActivityDate { get; set; }
        public DateTime LastLockoutDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        #endregion

        // Empty constructor required for EF to be able to create an entity object
        protected User() { }

        public User(string firstName, string lastName, string email, string profileImage, string bio)
        {
            _firstName = !string.IsNullOrWhiteSpace(firstName)
                ? firstName
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(firstName));

            _lastName = !string.IsNullOrWhiteSpace(lastName)
                ? lastName
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(lastName));

            _email = !string.IsNullOrWhiteSpace(email)
                ? email
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));

            _profileImage = !string.IsNullOrWhiteSpace(profileImage)
                ? profileImage
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(profileImage));

            _bio = !string.IsNullOrWhiteSpace(bio)
                ? bio
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(bio));

            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
            IsActive = true;

            // TODO: this are to be removed
            LastActivityDate = DateTime.Now;
            LastLockoutDate = DateTime.Now;
            LastLoginDate = DateTime.Now;
            LastPasswordChangeDate = DateTime.Now;
            Password = "temp";
            Salt = "temp";
        }

        public void SetDelete(bool status)
        {
            // We do not hard delete
            Deleted = status;

            if (Deleted)
            {
                IsActive = false;
            }
        }

        public void SetActive(bool status)
        {
            IsActive = status;
        }
    }
}
