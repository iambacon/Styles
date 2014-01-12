
namespace IAmBacon.Model.Common
{
    /// <summary>
    /// The Result interface.
    /// </summary>
    public interface IResult
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether success.
        /// </summary>
        bool Success { get; }

        #endregion
    }
}