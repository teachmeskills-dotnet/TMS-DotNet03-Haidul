using EventMaker.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventMaker.BLL.Interfaces
{
    /// <summary>
    /// Event manager.
    /// </summary>
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
        /// <param name="eventDto">Event DTO</param>
        Task EditEventAsync(EventDto eventDto);

        /// <summary>
        /// Delete event on index page.
        /// </summary>
        /// <param name="eventDto">Event DTO.</param>
        Task DeleteEventAsync(EventDto eventDto);

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
