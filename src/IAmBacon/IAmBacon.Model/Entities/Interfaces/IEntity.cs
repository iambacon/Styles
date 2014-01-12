using System;

namespace IAmBacon.Model.Entities.Interfaces
{
    /// <summary>
    /// The base entity interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        DateTime DateModified { get; set; }
    }
}