using System.Collections.Generic;
using EventMaker.BLL.Models;
using EventMaker.Common.Enums;

namespace EventMaker.BLL.Interfaces
{
    /// <summary>
    /// Filtration service.
    /// </summary>
    public interface IFiltrationService
    {
        /// <summary>
        /// Filter events.
        /// </summary>
        /// <param name="filterOptions">Filter options.</param>
        /// <param name="eventFormat">Format of event.</param>
        /// <param name="eventDtos">Collection of events.</param>
        /// <param name="name">Name.</param>
        /// <returns>IEnumerable<EventDto> </returns>
        IEnumerable<EventDto> FilterEvents(FilterOptions filterOptions, EventFormats eventFormat, IEnumerable<EventDto> eventDtos, string name = null);
    }
}
