using IAmBacon.Model.Entities;

namespace IAmBacon.Domain.Utilities.Interfaces
{
    /// <summary>
    /// Spam manager interface.
    /// </summary>
    public interface ISpamManager
    {
        /// <summary>
        /// Verifies if the comment is spam.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>Bool. True is comment is real.</returns>
        bool VerifyComment(Comment comment);
    }
}
