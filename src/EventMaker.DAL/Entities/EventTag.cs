using EventMaker.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMaker.DAL.Entities
{
    /// <summary>
    /// EventTag.
    /// </summary>
    public class EventTag : IHasDbIdentity
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Tag id.
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Navigation to Tag.
        /// </summary>
        public Tag Tag { get; set; }

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
