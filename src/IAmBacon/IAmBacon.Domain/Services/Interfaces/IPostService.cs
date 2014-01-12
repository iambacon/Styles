using System.Collections.Generic;
using IAmBacon.Model.Entities;

namespace IAmBacon.Domain.Services.Interfaces
{
    /// <summary>
    ///     The PostService interface.
    /// </summary>
    public interface IPostService : IService<Post>
    {
        /// <summary>
        ///     Gets the post by the specified seo title.
        /// </summary>
        /// <param name="seoTitle">The seo title.</param>
        /// <returns></returns>
        Post Get(string seoTitle);

        /// <summary>
        ///     Gets the latest posts.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> GetLatest();
    }
}