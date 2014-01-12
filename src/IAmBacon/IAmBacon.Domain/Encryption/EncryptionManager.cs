namespace IAmBacon.Domain.Encryption
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using IAmBacon.Domain.Encryption.Interfaces;
    using IAmBacon.Model.Common;

    /// <summary>
    /// The Encryption Manager.
    /// Handles the hashing and salting of passwords.
    /// </summary>
    public class EncryptionManager : IEncryptionManager
    {
        #region Constants and Fields

        /// <summary>
        /// The hash algorithm.
        /// </summary>
        private readonly HashAlgorithm hashAlgorithm;

        /// <summary>
        /// The salt length.
        /// </summary>
        private readonly int saltLength;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionManager" /> class.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="saltLength">The salt length.</param>
        public EncryptionManager(HashAlgorithm hashAlgorithm, int saltLength)
        {
            this.hashAlgorithm = hashAlgorithm;
            this.saltLength = saltLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionManager" /> class.
        /// </summary>
        public EncryptionManager()
            : this(new SHA256Managed(), 4)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a hash and a salt for the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The generated hash.</param>
        /// <param name="salt">The generated salt.</param>
        public void GetHashAndSalt(byte[] data, out byte[] hash, out byte[] salt)
        {
            salt = new byte[this.saltLength];

            var random = new RNGCryptoServiceProvider();

            random.GetNonZeroBytes(salt);

            hash = this.ComputeHash(data, salt);
        }

        /// <summary>
        /// Wrapper method for GetHashAndSalt.  Taking string values rather than byte arrays.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        public void GetHashAndSaltString(string data, out string hash, out string salt)
        {
            byte[] hashOut;
            byte[] saltOut;

            this.GetHashAndSalt(Encoding.UTF8.GetBytes(data), out hashOut, out saltOut);

            hash = Convert.ToBase64String(hashOut);
            salt = Convert.ToBase64String(saltOut);
        }

        /// <summary>
        /// Verifies if specified data and salt matches the specified hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public IResult VerifyHash(byte[] data, byte[] hash, byte[] salt)
        {
            var newHash = this.ComputeHash(data, salt);
            return new Result(newHash.SequenceEqual(hash));
        }

        /// <summary>
        /// Wrapper method for verify hash.  Taking string values rather than byte arrays.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public IResult VerifyHashString(string data, string hash, string salt)
        {
            var hashToVerify = Convert.FromBase64String(hash);
            var saltToVerify = Convert.FromBase64String(salt);
            var dataToVerify = Encoding.UTF8.GetBytes(data);

            return this.VerifyHash(dataToVerify, hashToVerify, saltToVerify);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The hash calculation.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>
        /// The calculated hash as byte array.
        /// </returns>
        private byte[] ComputeHash(byte[] data, byte[] salt)
        {
            var dataAndSalt = new byte[data.Length + this.saltLength];

            Array.Copy(data, dataAndSalt, data.Length);
            Array.Copy(salt, 0, dataAndSalt, data.Length, this.saltLength);

            return this.hashAlgorithm.ComputeHash(dataAndSalt);
        }

        #endregion
    }
}