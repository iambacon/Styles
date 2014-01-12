namespace IAmBacon.Presentation.Helpers
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The Encryption helper class. A static class containing helper methods for encryption and security.
    /// </summary>
    public static class EncryptionHelper
    {
        #region Methods

        /// <summary>
        ///     Creates a MD5 hash from the input value.
        /// </summary>
        /// <param name="input">The input value.</param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                builder = builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string
            return builder.ToString();
        }

        #endregion
    }
}