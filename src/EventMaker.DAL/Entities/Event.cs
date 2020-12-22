using System;
using System.Collections.Generic;
using EventMaker.Common.Enums;
using EventMaker.Common.Interfaces;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// Event.
    /// </summary>
    public class Event : IHasDbIdentity, IHasUserIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string UserId { get; set; }

        /// <summary>
        /// Navigation to User.
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
#nullable enable
        public string? AuthorName { get; set; }

        /// <summary>
        /// Title
        /// </summary>

        public string? Title { get; set; }

        /// <summary>
        /// Info.
        /// </summary>

        public string? Info { get; set; }

        /// <summary>
        /// Day of creation
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Start of the event.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// End of the event.
        /// </summary>
        public DateTime? Closed { get; set; }

        /// <summary>
        /// Format.
        /// </summary>
        public EventFormats? Format { get; set; }

        /// <summary>
        /// Participants number
        /// </summary>
        public int? PNumber { get; set; }

        /// <summary>
        /// Remaining free seats of participants.
        /// </summary>
        public int? PFreeNumber { get; set; }

        /// <summary>
        /// Navigation to Comments.
        /// </summary>

        public IEnumerable<Comment>? Comments { get; set; }

        /// <summary>
        /// Navigation to Event participants.
        /// </summary>

        public ICollection<EventParticipant>? EventParticipants { get; set; }

        /// <summary>
        /// Navigation to event tags.
        /// </summary>
        public ICollection<EventTag>? EventTags { get; set; }
    }
}
