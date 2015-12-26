using System.Collections.Generic;
using IAmBacon.Model.Entities;

namespace IAmBacon.Domain.Services.Interfaces
{
    /// <summary>
    /// The PostService interface.
    /// </summary>
    public interface IPostService : IService<Post>
    {
        /// <summary>
        /// Gets the post by the specified seo title.
        /// </summary>
        /// <param name="seoTitle">The seo title.</param>
        /// <returns>The post object.</returns>
        Post Get(string seoTitle);

        /// <summary>
        /// Returns a list of type <see cref="Post"/> that are active.
        /// </summary>
        /// <returns>A list of type <see cref="Post"/> that are active.</returns>
        IEnumerable<Post> GetAllActive();

            /// <summary>
        /// Gets the latest active posts.
        /// </summary>
        /// <param name="number">The number of posts to return.</param>
        /// <returns>
        /// Returns the specified number of posts, ordered descending by id.
        /// </returns>
        IEnumerable<Post> GetLatest(int number);

        /// <summary>
        /// Gets the popular active posts.
        /// </summary>
        /// <param name="number">The number of posts to return.</param>
        /// <returns>Returns the specified number of posts.</returns>
        IEnumerable<Post> GetPopular(int number);
    }
}