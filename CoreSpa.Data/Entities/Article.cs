using System.Collections.Generic;

namespace CoreSpa.Data.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public ICollection<ArticleRate> Rates { get; set; }

        public ICollection<ArticleAndTag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
