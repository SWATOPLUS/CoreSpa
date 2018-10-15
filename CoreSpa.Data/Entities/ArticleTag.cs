using System.Collections.Generic;

namespace CoreSpa.Data.Entities
{
    public class ArticleTag
    {
        public int ArticleTagId { get; set; }

        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }

        public ICollection<ArticleAndTag> Articles { get; set; }
    }
}
