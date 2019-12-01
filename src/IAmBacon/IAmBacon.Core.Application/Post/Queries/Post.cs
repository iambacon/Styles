using System;

namespace IAmBacon.Core.Application.Post.Queries
{
    public class Post
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Markdown { get; set; }
        public string Title { get; set; }
        public bool NoCss { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
