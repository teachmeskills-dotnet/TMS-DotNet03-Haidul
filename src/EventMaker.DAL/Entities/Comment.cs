using EventMaker.Common.Interfaces;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// Message.
    /// </summary>
    public class Comment : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public int EventId { get; set; }

        /// <summary>
        /// Author.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Message text.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Navigation to Event.
        /// </summary>
        public Event Event { get; set; }
    }
}
