﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Enums;
using EventMaker.DAL.Entities;

namespace EventMaker.BLL.Services
{
    public class FiltrationService : IFiltrationService
    {
        public IEnumerable<EventDto> FilterEvents(FilterOptions filterOptions,
                                                 EventFormats eventFormat,
                                                 IEnumerable<EventDto> eventDtos,
                                                 string name = null)
        {
            eventDtos = eventDtos.Where(ev => ev.Format == eventFormat);

            var result = filterOptions == FilterOptions.AuthorName ?
                                         eventDtos.Where(e => e.AuthorName.ToLower() == name.ToLower())
                                      :  eventDtos.Where(e => e.Name.ToLower() == name.ToLower());
            return result;
        }
    }
}
