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
    public class Category : Entity, IAggregateRoot
    {
        private DateTime _dateCreated;

        private DateTime _dateModified;

        private string _name;

        private string _seoName;

        public Category(string name)
        {
            _name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException("{name} cannot be null or empty", nameof(_name));
            _seoName = Seo.Title(name);
            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
        }
    }
}