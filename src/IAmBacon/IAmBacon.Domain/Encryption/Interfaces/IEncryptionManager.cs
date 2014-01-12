namespace IAmBacon.Domain.Encryption.Interfaces
{
    using IAmBacon.Model.Common;

    /// <summary>
    /// The Encryption Manager interface.
    /// </summary>
    public interface IEncryptionManager
    {
        #region Public Methods and Operators

        /// <summary>
        /// Returns a hash and a salt for the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The generated hash.</param>
        /// <param name="salt">The generated salt.</param>
        void GetHashAndSalt(byte[] data, out byte[] hash, out byte[] salt);

        /// <summary>
        /// Wrapper method for GetHashAndSalt.  Taking string values rather than byte arrays.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        void GetHashAndSaltString(string data, out string hash, out string salt);

        /// <summary>
        /// Verifies if specified data and salt matches the specified hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        IResult VerifyHash(byte[] data, byte[] hash, byte[] salt);

        /// <summary>
        /// Wrapper method for verify hash.  Taking string values rather than byte arrays.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// The <see cref="IResult"/>.
        /// </returns>
        IResult VerifyHashString(string data, string hash, string salt);

        #endregion
    }
}