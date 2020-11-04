namespace EventMaker.BLL.Models
{
    /// <summary>
    /// EventParticipant data transfer object.
    /// </summary>
    public class EventParticipantDto
    {
        /// <summary>
        /// UserId.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// EventId.
        /// </summary>
        public int EventId { get; set; }
    }
}
