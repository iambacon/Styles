using System;
using System.Collections.Generic;
using System.Linq;
using IAmBacon.ViewModels;

namespace IAmBacon.Web.Tests.Helpers
{
    /// <summary>
    /// Helper class for test assertions.
    /// </summary>
    public static class Assertions
    {
        /// <summary>
        /// Asserts List is sorted by date descending order.
        /// </summary>
        /// <param name="posts">The posts.</param>
        /// <exception cref="System.Exception"></exception>
        public static void ShouldBeSortedByDateInDescendingOrder(this IEnumerable<PostViewModel> posts)
        {
            var postsArray = posts.ToArray();
            for (var i = postsArray.Length - 1; i > 0; i--)
            {
                if (Convert.ToDateTime(postsArray[i].DateCreated) > Convert.ToDateTime(postsArray[i - 1].DateCreated))
                {
                    throw new Exception(
                        string.Format(
                            "Expected posts sorted in descending order, but found post \'{0}\' on {1} after \'{2}\' on {3}",
                            postsArray[i].Title, postsArray[i].DateCreated, postsArray[i - 1].Title,
                            postsArray[i - 1].DateCreated));
                }
            }
        }
    }
}
