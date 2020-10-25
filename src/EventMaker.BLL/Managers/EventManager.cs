using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.BLL.Managers
{
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
            var result = await _repositoryEvent.GetEntityWithoutTrackingAsync(e => e.Name == eventName);
            if (result != null)
            {
                return _mapper.Map<EventDto>(result);
            }
            throw new EventNotFoundException("Event not found");
        }

        public async Task CreateEventAsync(string userId, EventDto eventDto)
        {
            eventDto.UserId = userId;
            eventDto.Created = DateTime.UtcNow;
            var userEvent = _mapper.Map<Event>(eventDto);
            await _repositoryEvent.AddAsync(userEvent);
            await _repositoryEvent.SaveChangesAsync();
        }

        public async Task EditEventAsync(string userId, EventDto eventDto)
        {
            if (eventDto.UserId == userId)
            {
                var userEvent = _mapper.Map<Event>(eventDto);
                _repositoryEvent.Update(userEvent);
                await _repositoryEvent.SaveChangesAsync();
            }
            throw new EventNotFoundException("Event not found or you have no permissions to this action");
        }

        public async Task DeleteEventAsync(string userId, EventDto eventDto)
        {
            {
                eventDto.UserId = userId;
                var result = await _repositoryEvent.GetEntityAsync(evName => evName.Name.ToLower().Contains(eventDto.Name));
                if (result == null)
                {
                    result = await _repositoryEvent.GetEntityAsync(evTitle => evTitle.Title.ToLower().Contains(eventDto.Title));
                    if (result == null)
                    {
                        throw new EventNotFoundException("Event not found");
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

