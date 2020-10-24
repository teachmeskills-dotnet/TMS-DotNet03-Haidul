using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
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


        ///TODO: Add error handling;
        public async Task<EventDto> GetEventByName(string eventName)
        {
            var result = await _repositoryEvent.GetEntityWithoutTrackingAsync(e => e.Name == eventName);
            return _mapper.Map<EventDto>(result);
        }

        public async Task CreateEventAsync(string userId, EventDto eventDto)
        {
            eventDto.UserId = userId; // todo : try it on auto mapper profile
            eventDto.Created = DateTime.UtcNow;
            var userEvent = _mapper.Map<Event>(eventDto);
            await _repositoryEvent.AddAsync(userEvent);
            await _repositoryEvent.SaveChangesAsync();
        }

        public async Task EditEventAsync(string userId, EventDto eventDto)
        {
            if (eventDto.UserId == userId)// todo : try it on auto mapper profile
                                          //TODO : add human error handling
            {
                var userEvent = _mapper.Map<Event>(eventDto);
                _repositoryEvent.Update(userEvent);
                await _repositoryEvent.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(string userId, EventDto eventDto)
        {
            // todo : try it on auto mapper profile
            //TODO : add human error handling
            {
                eventDto.UserId = userId;
                //var userEvent = _mapper.Map<Event>(eventDto); - is that dont needed?
                var result = await _repositoryEvent.GetEntityAsync(evName => evName.Name.ToLower().Contains(eventDto.Name));
                if (result == null)
                {
                    result = await _repositoryEvent.GetEntityAsync(evTitle => evTitle.Title.ToLower().Contains(eventDto.Title));
                }
                _repositoryEvent.Delete(result);
                await _repositoryEvent.SaveChangesAsync();
            }
        }

        public IEnumerable<EventDto> GetAllEvents()
        {
            // todo : try it on auto mapper profile
            //TODO : add human error handling
            {
                var events = _repositoryEvent.GetAll().ToList();
                var userEvents = _mapper.Map<IEnumerable<EventDto>>(events);
                if (userEvents == null)
                {
                    return null;
                }
                else
                {
                    return userEvents;
                }
            }
        }

        
    }
}

