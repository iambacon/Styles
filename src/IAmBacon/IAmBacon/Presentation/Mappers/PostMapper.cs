namespace IAmBacon.Presentation.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Mvc;
    using Model.Entities;
    using Extensions;
    using ViewModels.Shared;

    using PagedList;

    /// <summary>
    /// Mapping class to map from <see cref="Post"/> to other models.
    /// </summary>
    public static class PostMapper
    {
        /// <summary>
        /// Maps <see cref="Post"/> to <see cref="PostThumbViewModel"/>.
        /// </summary>
        /// <param name="post">The <see cref="Post"/>.</param>
        /// <returns>The <see cref="PostThumbViewModel"/>.</returns>
        public static PostThumbViewModel ToViewModel(this Post post)
        {
            return new PostThumbViewModel
            {
                Title = post.Title,
                Thumbnail = post.Image.ToImageUrl(),
                DateTime = post.DateCreated.ToDateTimeFormat(),
                DisplayDate = post.DateCreated.ToDisplayDate(),
                Category = post.Category.Name,
                SeoTitle = post.SeoTitle,
                DisplayCategory = true,
                DisplayTags = false
            };
        }

        /// <summary>
        /// Maps <see cref="Post" /> to <see cref="PostThumbViewModel" />.
        /// </summary>
        /// <param name="post">The <see cref="Post" />.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="displayContent">if set to <c>true</c> [display content].</param>
        /// <returns>The <see cref="PostThumbViewModel" />.</returns>
        public static PostThumbViewModel ToViewModel(this Post post, IUrlHelper urlHelper, bool displayContent = false)
        {
            return new PostThumbViewModel
            {
                Title = post.Title,
                SeoTitle = post.SeoTitle,
                Content = post.Content.GetSummary(),
                DateCreated = post.DateCreated.ToDisplayDateTime(),
                DateTime = post.DateCreated.ToDateTimeFormat(),
                Tags = post.Tags.ToTagViewModelList(urlHelper),
                Category = post.Category.Name,
                Thumbnail = post.Image.ToImageUrl(),
                DisplayCategory = true,
                DisplayTags = false,
                DisplayContent = displayContent
            };
        }

        /// <summary>
        /// Maps a list of <see cref="Post" /> to a paged list of <see cref="PostThumbViewModel" />.
        /// </summary>
        /// <param name="posts">The the list of <see cref="Post" />.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The paged list of <see cref="PostThumbViewModel" />.</returns>
        public static IPagedList<PostThumbViewModel> ToPagedViewModelList(
            this IEnumerable<Post> posts,
            IUrlHelper urlHelper,
            int pageSize,
            int pageNumber)
        {
            return posts.Select(x => x.ToViewModel(urlHelper)).ToPagedList(pageNumber, pageSize);
        }

        /// <summary>
        /// Maps a list of <see cref="Post" /> to a paged list of <see cref="PostThumbViewModel" /> with content displayed.
        /// </summary>
        /// <param name="posts">The posts.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns></returns>
        public static IPagedList<PostThumbViewModel> ToPagedViewModelListWithContent(
            this IEnumerable<Post> posts,
            IUrlHelper urlHelper,
            int pageSize,
            int pageNumber)
        {
            return posts.Select(x => x.ToViewModel(urlHelper, true)).ToPagedList(pageNumber, pageSize);
        }
    }
}
