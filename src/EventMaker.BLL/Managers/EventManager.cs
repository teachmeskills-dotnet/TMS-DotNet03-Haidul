using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.DAL.Entities;

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

        public async Task<EventDto> GetEventById(int eventId, string userId = null)
        {
            var result = userId != null ? await _repositoryEvent.GetEntityAsync(e => e.Id == eventId && e.UserId == userId)
                                        : await _repositoryEvent.GetEntityAsync(e => e.Id == eventId);
            if (result != null)
            {
                return _mapper.Map<EventDto>(result);
            }
            else
            {
                throw new NotFoundException(ExceptionResource.EventNotFound);
            }
        }

        public async Task CreateEventAsync(EventDto eventDto)
        {
            if (eventDto != null)
            {
                var allEvents = _repositoryEvent.GetAllWithoutTracking();
                if (allEvents.FirstOrDefault(ev => ev.Name == eventDto.Name) != null)
                {
                    throw new OtherException<string>(ExceptionResource.NameAlreadyExist);
                }
                else
                {
                    eventDto.Created = DateTime.UtcNow;
                    eventDto.PFreeNumber = eventDto.PNumber;
                    var userEvent = _mapper.Map<Event>(eventDto);
                    await _repositoryEvent.AddAsync(userEvent);
                    await _repositoryEvent.SaveChangesAsync();
                }
            }
            else
            {
                throw new OtherException<string>(ExceptionResource.NotCreated);
            }
        }

        public async Task UpdateEventAsync(EventDto eventDto)
        {
            if (eventDto != null)
            {
                var userEvent = await _repositoryEvent.GetEntityAsync(ev => ev.Id == eventDto.Id && ev.UserId == eventDto.UserId);

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
                        userEvent.PFreeNumber += (dtoEvent.PNumber - userEvent.PNumber);
                        userEvent.PNumber = dtoEvent.PNumber;
                        updated = true;
                    }
                    else if (userEvent.PNumber > dtoEvent.PNumber)
                    {
                        userEvent.PFreeNumber = userEvent.PFreeNumber - (userEvent.PNumber - dtoEvent.PNumber);
                        userEvent.PNumber = dtoEvent.PNumber;
                        updated = true;

                        if (userEvent.PFreeNumber < 0)
                        {
                            userEvent.PFreeNumber = 0;
                            throw new EventOwerflowException(ExceptionResource.OwerflowException);
                        }
                    }
                    else
                    {
                        userEvent.PFreeNumber = dtoEvent.PFreeNumber;
                        updated = true;
                    }

                    return updated;
                }

                var result = ValidateToUpdate(userEvent, eventDto);
                if (result)
                {
                    await _repositoryEvent.SaveChangesAsync();
                }
            }
            else
            {
                throw new NotFoundException(ExceptionResource.EventNotFound);
            }
        }

        public async Task DeleteEventAsync(EventDto eventDto)
        {
            if (eventDto != null)
            {
                var result = await _repositoryEvent.GetEntityAsync(evId => evId.Id == eventDto.Id);
                if (result == null)
                {
                    throw new NotFoundException(ExceptionResource.EventNotFound);
                }
                _repositoryEvent.Delete(result);
                await _repositoryEvent.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException(ExceptionResource.EventNotFound);
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
                var result = eventParticipantsDto.Where(evPart => evPart.EventId == eventId);
                return result;
            }
            throw new NotFoundException(ExceptionResource.EventNotFound);
        }

        public async Task AddParticipantAsync(int eventId, string userId, EventDto eventDto)
        {
            if (eventDto != null)
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
                    throw new OtherException<string>(ExceptionResource.AlreadyParticipant);
                }
            }
        }

        public async Task DeleteParticipantAsync(int eventId, string userId, EventDto eventDto)
        {
            if (eventDto != null)
            {
                var eventParticipant = await _repositoryEventParticipant.GetEntityWithoutTrackingAsync(evPar => evPar.UserId == userId
                                                                                                        && evPar.EventId == eventId);
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
                        throw new OtherException<string>(ExceptionResource.NotDeleted);
                    }
                }
                else
                {
                    throw new OtherException<string>(ExceptionResource.AlreadyParticipant);
                }
            }
        }
    }
}
