using System;

namespace IAmBacon.Core.Application.Post.Commands
{
    public class UpdatePostCommand
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; }
        public int AuthorId { get; }
        public int CategoryId { get; }
        public string Image { get; set; }
        public bool NoCss { get; set; }
        public int[] TagIds { get; set; }
        public string Title { get; }
        public string Content { get; }

        public UpdatePostCommand(int id, int authorId, int categoryId, string title, string content)
        {
            if (authorId <= 0) throw new ArgumentOutOfRangeException(nameof(authorId));
            if (categoryId <= 0) throw new ArgumentOutOfRangeException(nameof(categoryId));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            Id = id;
            AuthorId = authorId;
            CategoryId = categoryId;
            Title = title;
            Content = content;
        }
    }
}
