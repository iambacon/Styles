namespace IAmBacon.Presentation.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using IAmBacon.Framework.Mvc;
    using IAmBacon.Model.Entities;
    using IAmBacon.Presentation.Extensions;
    using IAmBacon.ViewModels;

    using PagedList;

    /// <summary>
    /// Mapping class to map from <see cref="Post"/> to other models.
    /// </summary>
    public static class PostMapper
    {
        /// <summary>
        /// Maps <see cref="Post"/> to <see cref="PostViewModel"/>.
        /// </summary>
        /// <param name="post">The <see cref="Post"/>.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The <see cref="PostViewModel"/>.</returns>
        public static PostViewModel ToViewModel(this Post post, IUrlHelper urlHelper)
        {
            return new PostViewModel
            {
                Title = post.Title,
                SeoTitle = post.SeoTitle,
                Content = post.Content.GetSummary(),
                DateCreated = post.DateCreated.ToDisplayDateTime(),
                DateTime = post.DateCreated.ToDateTimeFormat(),
                Tags = post.Tags.ToTagViewModelList(urlHelper),
                Author = string.Join(" ", post.User.FirstName, post.User.LastName),
                Category = post.Category.Name,
                Id = post.Id
            };
        }

        /// <summary>
        /// Maps a list of <see cref="Post" /> to a paged list of <see cref="PostViewModel" />.
        /// </summary>
        /// <param name="posts">The the list of <see cref="Post" />.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The paged list of <see cref="PostViewModel" />.</returns>
        public static IPagedList<PostViewModel> ToPagedViewModelList(
            this IEnumerable<Post> posts,
            IUrlHelper urlHelper,
            int pageSize,
            int pageNumber)
        {
            return posts.Select(x => x.ToViewModel(urlHelper)).ToPagedList(pageNumber, pageSize);
        }
    }
}
