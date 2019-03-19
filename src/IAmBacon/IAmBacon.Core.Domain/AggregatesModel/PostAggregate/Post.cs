using System;
using System.Collections.Generic;
using IAmBacon.Core.Domain.Base;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    public class Post : Entity, IAggregateRoot, IDeleteable
    {
        private readonly int _authorId;
        private readonly int _categoryId;
        private readonly string _title;
        private readonly string _content;
        private string _image;
        private bool _noCss;
        private List<Tag> _tags;

        public bool IsActive { get; private set; }

        public bool Deleted { get; private set; }

        // Empty constructor required for EF to be able to create an entity object
        protected Post() { }

        public Post(int authorId, int categoryId, string title, string content)
        {
            if (authorId <= 0) throw new ArgumentOutOfRangeException(nameof(authorId));
            if (categoryId <= 0) throw new ArgumentOutOfRangeException(nameof(categoryId));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            _authorId = authorId;
            _categoryId = categoryId;
            _title = title;
            _content = content;
            IsActive = true;
        }

        public void SetDelete(bool status)
        {
            // We do not hard delete
            Deleted = status;

            if (Deleted)
            {
                IsActive = false;
            }
        }

        public void SetActive(bool status)
        {
            IsActive = status;
        }

        public void SetImage(string image)
        {
            if (string.IsNullOrWhiteSpace(image)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(image));

            _image = image;
        }

        public void SetNoCss(bool noCss)
        {
            _noCss = noCss;
        }

        public void SetTags(List<Tag> tags)
        {
            _tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }
    }
}
