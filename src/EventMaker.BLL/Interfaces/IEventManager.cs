using EventMaker.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.BLL.Interfaces
{
    public interface IEventManager
    {
        /// <summary>
        /// Create Event.
        /// </summary>
        /// <param name="userId">string (GUID).</param>
        /// <param name="eventDto">Event DTO.</param>
        Task CreateEventAsync(string userId, EventDto eventDto);

        /// <summary>
        /// Edit event.
        /// </summary>
        /// <param name="userId">string (GUID).</param>
        /// <param name="eventDto">Event DTO</param>
        Task EditEventAsync(string userId, EventDto eventDto);

        /// <summary>
        /// Delete event.
        /// </summary>
        /// <param name="userId">string (GUID).</param>
        /// <param name="eventName">Event name.</param>
        /// <param name="eventTitle">Event title.</param>
        /// <param name="eventDto">Event DTO.</param>
        Task DeleteEventAsync(string userId, EventDto eventDto);

        /// <summary>
        /// Get event by name.
        /// </summary>
        /// <param name="eventName">Event name.</param>
        /// <returns>Task<EventDto></returns>
        Task<EventDto> GetEventByName(string eventName);

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns> IEnumerable<EventDto></returns>
        IEnumerable<EventDto> GetAllEvents();
    }
}
