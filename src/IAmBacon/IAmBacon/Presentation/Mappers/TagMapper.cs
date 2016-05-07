namespace IAmBacon.Presentation.Mappers
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Mvc;
    using Model.Entities;
    using Extensions;
    using ViewModels.Post;

    /// <summary>
    /// Mapping class to map <see cref="Tag"/> to other models.
    /// </summary>
    public static class TagMapper
    {
        /// <summary>
        /// Maps <see cref="Tag"/> to <see cref="TagViewModel"/>.
        /// </summary>
        /// <param name="tag">The <see cref="Tag"/>.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The <see cref="TagViewModel"/>.</returns>
        public static TagViewModel ToViewModel(this Tag tag, IUrlHelper urlHelper)
        {
            return new TagViewModel
            {
                Name = tag.Name,
                Url = urlHelper.Tag(tag.SeoName)
            };
        }

        /// <summary>
        /// Maps a list of <see cref="Tag"/> to a list of <see cref="TagViewModel"/>.
        /// </summary>
        /// <param name="tags">The List of <see cref="Tag"/>.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The list of <see cref="TagViewModel"/>.</returns>
        public static IEnumerable<TagViewModel> ToTagViewModelList(
            this IEnumerable<Tag> tags,
            IUrlHelper urlHelper)
        {
            return tags.Select(x => x.ToViewModel(urlHelper));
        }
    }
}
