using EventMaker.Common.Interfaces;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// Event participants.
    /// </summary>
    public class EventParticipant : IHasDbIdentity, IHasUserIdentity
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
        /// Event id.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Navigation to Event.
        /// </summary>
        public Event Event { get; set; }
    }
}
