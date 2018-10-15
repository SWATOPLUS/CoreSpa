using System.Collections.Generic;

namespace CoreSpa.Data.Entities
{
    public class ArticleAndTag
    {
        public int ArticleAndTagId { get; set; }

        public int ArticleTagId { get; set; }

        public ArticleTag Tag { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
