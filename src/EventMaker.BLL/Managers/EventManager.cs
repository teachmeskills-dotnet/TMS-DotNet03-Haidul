using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.BLL.Managers
{
    /// <inheritdoc cref="IEventManager"/>
    public class EventManager : IEventManager
    {
        private readonly IRepository<Event> _repositoryEvent;
        private readonly IRepository<EventParticipant> _repositoryEventParticipant;
        private readonly IMapper _mapper;

        public EventManager(IRepository<Event> repositoryEvent,
                            IMapper mapper,
                            IRepository<EventParticipant> repositoryEventParticipant)
        {
            _repositoryEvent = repositoryEvent ?? throw new ArgumentNullException(nameof(repositoryEvent));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositoryEventParticipant = repositoryEventParticipant ?? throw new ArgumentNullException(nameof(repositoryEventParticipant));
        }

        public async Task<EventDto> GetEventByName(string eventName)
        {
            var result = await _repositoryEvent.GetEntityAsync(e => e.Name == eventName);
            if (result != null)
            {
                return _mapper.Map<EventDto>(result);
            }
            else
            {
                throw new EventNotFoundException(ExceptionResource.EventNotFound);
            }

        }

        public async Task<EventDto> GetEventById(int eventId, string userId)
        {
            var result = await _repositoryEvent.GetEntityAsync(e => e.Id == eventId && e.UserId == userId);
            if (result != null)
            {
                return _mapper.Map<EventDto>(result);
            }
            else
            {
                throw new EventNotFoundException(ExceptionResource.EventNotFound);
            }

        }

        public async Task CreateEventAsync(EventDto eventDto)
        {
            eventDto.Created = DateTime.UtcNow;
            var userEvent = _mapper.Map<Event>(eventDto);
            await _repositoryEvent.AddAsync(userEvent);
            await _repositoryEvent.SaveChangesAsync();
        }

        public async Task UpdateEventAsync(EventDto dtoEvent)
        {
            if (dtoEvent != null)
            {
                var userEvent = await _repositoryEvent.GetEntityAsync(ev => ev.Id == dtoEvent.Id && ev.UserId == dtoEvent.UserId);

                static bool ValidateToUpdate(Event userEvent, EventDto dtoEvent)
                {
                    bool updated = false;

                    if (userEvent.Title != dtoEvent.Title)
                    {
                        userEvent.Title = dtoEvent.Title;
                        updated = true;
                    }

                    if (userEvent.Info != dtoEvent.Info)
                    {
                        userEvent.Info = dtoEvent.Info;
                        updated = true;
                    }

                    if (userEvent.Format != dtoEvent.Format)
                    {
                        userEvent.Format = dtoEvent.Format;
                        updated = true;
                    }

                    if (userEvent.Name != dtoEvent.Name)
                    {
                        userEvent.Name = dtoEvent.Name;
                        updated = true;
                    }

                    if (userEvent.Started != dtoEvent.Started)
                    {
                        userEvent.Started = dtoEvent.Started;
                        updated = true;
                    }

                    if (userEvent.PNumber != dtoEvent.PNumber)
                    {
                        userEvent.PNumber = dtoEvent.PNumber;
                        updated = true;
                    }

                    if (userEvent.PFreeNumber != dtoEvent.PFreeNumber)
                    {
                        userEvent.PFreeNumber = dtoEvent.PFreeNumber;
                        updated = true;
                    }

                    return updated;
                }

                var result = ValidateToUpdate(userEvent, dtoEvent);
                if (result)
                {
                    await _repositoryEvent.SaveChangesAsync();
                }
            }
            else
            {
                throw new EventNotFoundException(ExceptionResource.EventNotFound);
            }
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
            var events = _repositoryEvent.GetAllWithoutTracking().ToList();
            var userEvents = _mapper.Map<IEnumerable<EventDto>>(events);
            return userEvents;
        }

        public IEnumerable<EventParticipantDto> GetAllParticipants(int eventId)
        {
            if (eventId != 0)
            {
                var eventParticipants = _repositoryEventParticipant.GetAllWithoutTracking().ToList();
                var eventParticipantsDto = _mapper.Map<IEnumerable<EventParticipant>, IEnumerable<EventParticipantDto>>(eventParticipants);
                eventParticipantsDto.Where(evPart => evPart.EventId == eventId); 
                return eventParticipantsDto;
            }
            throw new EventNotFoundException(ExceptionResource.EventNotFound);
        }

        public async Task AddParticipantAsync(int eventId, string userId, EventDto eventDto)
        {
            var isParticipant = await _repositoryEventParticipant.GetEntityWithoutTrackingAsync(evPar => evPar.UserId == userId && evPar.EventId == eventId);
            if (eventDto.PFreeNumber != 0 && isParticipant == null)
            {
                var participant = new EventParticipant
                {
                    EventId = eventId,
                    UserId = userId
                };
                await _repositoryEventParticipant.AddAsync(participant);
                await _repositoryEventParticipant.SaveChangesAsync();
                --eventDto.PFreeNumber;
                await UpdateEventAsync(eventDto);
            }
            else
            {
                throw new AlreadyParticipantException(ExceptionResource.AlreadyParticipant);
            }

        }

        public async Task DeleteParticipantAsync(int eventId, string userId, EventDto eventDto)
        {
            var eventParticipant = await _repositoryEventParticipant.GetEntityWithoutTrackingAsync(evPar => evPar.UserId == userId && evPar.EventId == eventId);
            if (eventParticipant != null)
            {
                _repositoryEventParticipant.Delete(eventParticipant);
                await _repositoryEventParticipant.SaveChangesAsync();
                ++eventDto.PFreeNumber;
                if (eventDto.PFreeNumber <= eventDto.PNumber)
                {
                    await UpdateEventAsync(eventDto);
                }
                else
                {
                    throw new OtherException(ExceptionResource.NotDeleted);
                }
            }
            else
            {
                throw new AlreadyParticipantException(ExceptionResource.AlreadyParticipant);
            }
        }
    }
}