using System;
using System.Collections.Generic;

namespace CoreSpa.Data.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        public int ArticleId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Content { get; set; }

        public ICollection<CommentLike> Likes { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
