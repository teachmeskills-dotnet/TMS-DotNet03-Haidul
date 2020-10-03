using Microsoft.AspNetCore.Identity;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// User.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Navigation to Profile.
        /// </summary>
        public Profile Profile { get; set; }
    }
}
