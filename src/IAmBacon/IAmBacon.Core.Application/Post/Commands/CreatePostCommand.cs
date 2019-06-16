using System;

namespace IAmBacon.Core.Application.Post.Commands
{
    public class CreatePostCommand
    {
        public bool IsActive { get; set; }
        public int AuthorId { get; }
        public int CategoryId { get; }
        public string Image { get; set; }
        public bool NoCss { get; set; }
        public int[] TagIds { get; set; }
        public string Title { get; }
        public string Content { get; }

        public CreatePostCommand(int authorId, int categoryId, string title, string content)
        {
            if (authorId <= 0) throw new ArgumentOutOfRangeException(nameof(authorId));
            if (categoryId <= 0) throw new ArgumentOutOfRangeException(nameof(categoryId));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            AuthorId = authorId;
            CategoryId = categoryId;
            Title = title;
            Content = content;
        }
    }
}
