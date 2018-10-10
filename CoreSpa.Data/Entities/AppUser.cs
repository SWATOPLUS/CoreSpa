using Microsoft.AspNetCore.Identity;

namespace CoreSpa.Data.Entities
{
    // Add profile data for application users by adding properties to this class
    public class AppUser : IdentityUser
    {
        // Extended Properties
        public long? FacebookId { get; set; }
    }
}
