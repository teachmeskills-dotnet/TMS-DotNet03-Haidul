using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.BLL.Managers
{
    /// <inheritdoc cref="IEventManager"/>
    public class EventManager : IEventManager
    {
        private readonly IRepository<Event> _repositoryEvent;
        private readonly IMapper _mapper;

        public EventManager(IRepository<Event> repositoryEvent,
            IMapper mapper)
        {
            _repositoryEvent = repositoryEvent ?? throw new ArgumentNullException(nameof(repositoryEvent));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EventDto> GetEventByName(string eventName)
        {
            var result = await _repositoryEvent.GetEntityAsync(e => e.Name == eventName);
            if (result != null)
            {
                return _mapper.Map<EventDto>(result);
            }
            throw new EventNotFoundException(ExceptionResource.EventNotFound);
        }

        public async Task CreateEventAsync(string userId, EventDto eventDto)
        {
            eventDto.UserId = userId;
            eventDto.Created = DateTime.UtcNow;
            var userEvent = _mapper.Map<Event>(eventDto);
            await _repositoryEvent.AddAsync(userEvent);
            await _repositoryEvent.SaveChangesAsync();
        }

        public async Task EditEventAsync(EventDto eventDto)
        {
            if (eventDto != null)
            {
                var userEvent = _mapper.Map<Event>(eventDto);
                _repositoryEvent.Update(userEvent);
                await _repositoryEvent.SaveChangesAsync();
            }
            throw new EventNotFoundException(ExceptionResource.EventNotFound);
        }

        public async Task DeleteEventAsync(EventDto eventDto)
        {
            {
                var result = await _repositoryEvent.GetEntityAsync(evName => evName.Name.ToLower().Contains(eventDto.Name));
                if (result == null)
                {
                    result = await _repositoryEvent.GetEntityAsync(evTitle => evTitle.Title.ToLower().Contains(eventDto.Title));
                    if (result == null)
                    {
                        throw new EventNotFoundException(ExceptionResource.EventNotFound);
                    }
                }
                _repositoryEvent.Delete(result);
                await _repositoryEvent.SaveChangesAsync();
            }
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            {
                var events = _repositoryEvent.GetAll().ToList();
                var userEvents = _mapper.Map<IEnumerable<EventDto>>(events);
                return userEvents;
            }
        }
    }
}

