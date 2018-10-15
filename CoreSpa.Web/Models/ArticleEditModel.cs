using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSpa.Data.Entities;

namespace CoreSpa.Web.Models
{
    public class ArticleEditModel
    {
        public int ArticleId { get; set; }

        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string[] TagsDisplayNames { get; set; }
    }
}
