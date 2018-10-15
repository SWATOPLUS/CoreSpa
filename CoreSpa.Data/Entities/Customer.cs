using System.Collections.Generic;

namespace CoreSpa.Data.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property

        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string PictureUrl { get; set; }

        public ICollection<Article> Articles { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
