namespace IAmBacon.Model.Entities.Interfaces
{
    /// <summary>
    /// The Tag interface.
    /// </summary>
    public interface ITag : IEntity
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        #endregion
    }
}