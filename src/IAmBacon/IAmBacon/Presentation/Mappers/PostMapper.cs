namespace IAmBacon.Presentation.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using IAmBacon.Framework.Mvc;
    using IAmBacon.Model.Entities;
    using IAmBacon.Presentation.Extensions;
    using IAmBacon.ViewModels;

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
        /// Maps a list of <see cref="Post"/> to a list of <see cref="PostViewModel"/>.
        /// </summary>
        /// <param name="posts">The the list of <see cref="Post"/>.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The list of <see cref="PostViewModel"/>.</returns>
        public static IEnumerable<PostViewModel> ToViewModelList(
            this IEnumerable<Post> posts,
            IUrlHelper urlHelper)
        {
            return posts.Select(x => x.ToViewModel(urlHelper));
        }
    }
}
