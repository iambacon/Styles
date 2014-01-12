namespace IAmBacon.Model.Entities.Interfaces
{
    /// <summary>
    /// The Category interface.
    /// </summary>
    public interface ICategory : IEntity
    {
        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        string Name { get; set; }
    }
}
