using CoreSpa.Data.Entities;

namespace CoreSpa.Web.Models
{
    public class ArticleSummary
    {
        public int ArticleId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerDisplayName { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public int RatesCount { get; set; }

        public double RatesAverage { get; set; }

        public ArticleTag[] Tags { get; set; }

        public int CommentsCount { get; set; }
    }
}
