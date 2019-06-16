using System;

namespace IAmBacon.Core.Domain.AggregatesModel.PostAggregate
{
    /// <summary>
    /// This is a linking table to describe the many to many relationship between Post and Tag entities.
    /// In DDD terminology it is my understanding that this would be described as a value object.
    /// </summary>
    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }

        public PostTag(int postId, int tagId)
        {
            if (postId <= 0) throw new ArgumentOutOfRangeException(nameof(postId));
            if (tagId <= 0) throw new ArgumentOutOfRangeException(nameof(tagId));

            PostId = postId;
            TagId = tagId;
        }
    }
}