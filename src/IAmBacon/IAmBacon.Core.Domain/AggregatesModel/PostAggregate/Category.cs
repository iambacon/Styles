using System;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Domain.Utilities;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    /// <summary>
    /// This is the Category Entity.
    /// It must hold the Entity's data attributes as well as the behaviour and logic.
    /// This is an Aggregate Root as well because it is this Entity that will added to the DB.
    /// If this Entity had child Entities they would not be Aggregate roots
    /// </summary>
    public class Category : Entity, IAggregateRoot, IDeleteable
    {
        private DateTime _dateCreated;

        private DateTime _dateModified;

        private string _name;

        private string _seoName;

        private bool _active;

        public bool IsActive => _active;

        public bool IsDeleted { get; private set; }

        // Empty constructor required for EF to be able to create an entity object
        protected Category() { }

        public Category(string name)
        {
            _name = ValidateName(name);
            _seoName = Seo.Title(name);
            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
            _active = true;
        }

        public void SetDeleteStatus()
        {
            // We do not delete a category as it may be associated with a post
            _active = false;
            IsDeleted = true;
        }

        public void SetName(string name)
        {
            _name = ValidateName(name);
            _seoName = Seo.Title(name);
            _dateModified = DateTime.Now;
        }

        private string ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
    }
}