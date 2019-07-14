using System;
using System.Collections.Generic;
using System.Linq;
using IAmBacon.Core.Domain.Base;
using IAmBacon.Core.Domain.Utilities;
using Markdig;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    /// <summary>
    /// This is the Post Entity.
    /// It must hold the Entity's data attributes as well as the behaviour and logic.
    /// This is an Aggregate Root as well because it is this Entity that will added to the DB.
    /// If this Entity had child Entities they would not be Aggregate roots
    /// </summary>
    public class Post : Entity, IAggregateRoot, IDeleteable
    {
        private DateTime _dateCreated;
        private DateTime _dateModified;
        private int _authorId;
        private string _title;
        private string _content;
        private string _markdown;
        private string _seoTitle;
        private string _image;
        private bool _noCss;
        private int[] _tagIds;
        private ICollection<PostTag> _postTags;
        private int _categoryId;

        public ICollection<PostTag> PostTags => _postTags;

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
            _seoTitle = Seo.Title(title);
            _dateCreated = DateTime.Now;
            _dateModified = _dateCreated;
            _postTags = new List<PostTag>();
            SetMarkdown();
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

        public void SetTags(int[] tagIds)
        {
            _tagIds = tagIds;
            UpdatePostTags();
        }

        private void UpdatePostTags()
        {
            PostTags.Clear();

            foreach (var tagId in _tagIds)
            {
                if (PostTags.Any(x => x.TagId == tagId))
                    continue;

                var postTag = new PostTag(Id, tagId);
                PostTags.Add(postTag);
            }
        }

        private void SetMarkdown()
        {
            // Maybe create a wrapper class for this?
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            _markdown = Markdown.ToHtml(_content, pipeline);
        }

        public void SetAuthor(int authorId)
        {
            if (authorId <= 0) throw new ArgumentOutOfRangeException(nameof(authorId));

            _authorId = authorId;
        }

        public void SetCategory(int categoryId)
        {
            if (categoryId <= 0) throw new ArgumentOutOfRangeException(nameof(categoryId));

            _categoryId = categoryId;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(title));

            _title = title;
        }

        public void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(content));

            _content = content;
        }
    }
}
