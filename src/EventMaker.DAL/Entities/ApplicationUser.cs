using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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

        /// <summary>
        /// Navigation to Event Participants.
        /// </summary>
        public ICollection<EventParticipant> EventParticipants { get; set; }

        /// <summary>
        /// Navigation to Events.
        /// </summary>
        public ICollection<Event> Events { get; set; }
    }
}
