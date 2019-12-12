using System;
using System.Collections.Generic;
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

        private string _seoName;

        private bool _active;

        private ICollection<Post> _posts;

        public bool IsActive => _active;

        public ICollection<Post> Posts => _posts;

        public bool Deleted { get; private set; }

        public string Name { get; private set; }

        // Empty constructor required for EF to be able to create an entity object
        protected Category() { }

        public Category(string name)
        {
            Name = ValidateName(name);
            _seoName = Seo.Title(name);
            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
            _active = true;
        }

        public void SetDelete(bool status)
        {
            // We do not delete a category as it may be associated with a post
            Deleted = status;

            if (Deleted)
            {
                _active = false;
            }
        }

        public void SetName(string name)
        {
            Name = ValidateName(name);
            _seoName = Seo.Title(name);
            _dateModified = DateTime.Now;
        }

        public void SetActive(bool status)
        {
            _active = status;
        }

        private string ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
    }
}