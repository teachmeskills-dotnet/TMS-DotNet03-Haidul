using EventMaker.BLL.Interfaces;
using EventMaker.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace EventMaker.BLL.Managers
{
    public class EventManager : IEventManager
    {
        private readonly IRepository<Event> _repositoryEvent;

        public EventManager(IRepository<Event> repositoryEvent)
        {
            _repositoryEvent = repositoryEvent ?? throw new ArgumentNullException(nameof(repositoryEvent));
        }


        ///ToDo: ask about automapper and use it if possible;
        public async Task CreateEventAsync(string userId)
        {
            var userEvent = new Event
            {

            };
            await _repositoryEvent.AddAsync(userEvent);
            await _repositoryEvent.SaveChangesAsync();
        }

    }
}
