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
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private bool _active;

        public bool IsActive => _active;
        public bool Deleted { get; }

        // Empty constructor required for EF to be able to create an entity object
        protected User() { }

        public User(string firstName, string lastName, string email)
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

            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
        }
    }
}
