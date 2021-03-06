﻿using System.Collections.Generic;
using System.Linq;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Enums;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;

namespace EventMaker.BLL.Services
{
    /// <summary>
    /// <inheritdoc cref="IFiltrationService"/>
    /// </summary>
    public class FiltrationService : IFiltrationService
    {
        public IEnumerable<EventDto> FilterEvents(FilterOptions? filterOptions,
                                                 EventFormats? eventFormat,
                                                 IEnumerable<EventDto> eventDtos,
                                                 string name = null)
        {
            if (eventDtos != null)
            {
                if (eventFormat != null)
                {
                    eventDtos = eventDtos.Where(ev => ev.Format == eventFormat);
                }
                if (filterOptions != null)
                {
                    var result = filterOptions == FilterOptions.AuthorName ?
                                            eventDtos.Where(e => e.AuthorName.ToLower() == name.ToLower())
                                         : eventDtos.Where(e => e.Name.ToLower() == name.ToLower());
                    return result;
                }
                else
                {
                    eventDtos = eventDtos.Where(ev => ev.AuthorName == name);
                    return eventDtos;
                }
            }
            else
            {
                throw new NotFoundException(ExceptionResource.EventNotFound);
            }
        }
    }
}
