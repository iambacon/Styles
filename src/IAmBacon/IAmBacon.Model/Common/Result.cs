
namespace IAmBacon.Model.Common
{
    /// <summary>
    /// The result.
    /// </summary>
    public class Result : IResult
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        public Result(bool success)
        {
            this.Success = success;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether success.
        /// </summary>
        public bool Success { get; private set; }

        #endregion
    }
}