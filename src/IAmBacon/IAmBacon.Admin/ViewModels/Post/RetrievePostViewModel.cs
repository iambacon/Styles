using System;
using System.Collections.Generic;

namespace IAmBacon.Admin.ViewModels.Post
{
    public class RetrievePostViewModel
    {
        public string Author { get; set; }
        public bool Active { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public bool NoCss { get; set; }
        public string Summary { get; set; }
        public virtual IReadOnlyCollection<Core.Application.PostTag.Queries.Tag> Tags { get; set; }
        public string Title { get; set; }
    }
}
