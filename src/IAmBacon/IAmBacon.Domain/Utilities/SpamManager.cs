using IAmBacon.Domain.Utilities.Interfaces;
using IAmBacon.Model.Entities;

namespace IAmBacon.Domain.Utilities
{
    /// <summary>
    /// Spam manager.
    /// Verifies comments to check if spam messages.
    /// </summary>
    public class SpamManager: ISpamManager
    {
        /// <summary>
        /// Verifies if the comment is spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// Bool. True is comment is real.
        /// </returns>
        public bool VerifyComment(Comment comment)
        {
            return VerifyName(comment.Name);
        }

        /// <summary>
        /// Verifies if the name is considered spam.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static bool VerifyName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                // smells like spam if name contains http.
                return !name.Contains("http");
            }

            return false;
        }
    }
}
