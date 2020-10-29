using EventMaker.BLL.Models;
using System.Collections.Generic;
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
        /// <param name="eventDto">Event DTO.</param>
        Task CreateEventAsync(EventDto eventDto);

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
        /// Get event by id.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns>Task<EventDto></returns>
        Task<EventDto> GetEventById(int eventId, string userId);

        /// <summary>
        /// Get all events.
        /// </summary>
        /// <returns> IEnumerable<EventDto></returns>
        IEnumerable<EventDto> GetAllEvents();
    }
}