namespace IAmBacon.Models
{
    /// <summary>
    /// The checkbox item model.
    /// </summary>
    public class CheckboxItem
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is checked].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is checked]; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        #endregion
    }
}