using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using System;
using System.Linq.Expressions;
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

        public async Task CreateEventAsync(string userId, EventDto eventDto)
        {
            eventDto.UserId = userId; // todo : try it on auto mapper profile
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
                var userEvent = _mapper.Map<Event>(eventDto);
                Expression<Func<Event, bool>> exprName = evName => evName.Name.ToLower().Contains(eventDto.Name);
                Expression<Func<Event, bool>> exprTitle = evTitle => evTitle.Title.ToLower().Contains(eventDto.Title);
                var result = await _repositoryEvent.GetEntityAsync(exprName);
                if (result == null)
                {
                    result = await _repositoryEvent.GetEntityAsync(exprTitle);
                }
                _repositoryEvent.Delete(result);
                await _repositoryEvent.SaveChangesAsync();
            }
        }
    }
}

