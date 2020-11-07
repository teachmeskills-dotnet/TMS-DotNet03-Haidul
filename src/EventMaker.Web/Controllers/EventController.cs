using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Common.Exceptions;
using EventMaker.Common.Resources;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventMaker.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IEventManager _eventManager;
        private readonly IMapper _mapper;

        public EventController(IAccountManager accountManager,
                              IEventManager eventManager,
                              IMapper mapper)
        {
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]////TODO : Refactor it;
        public async Task<IActionResult> Index(string name)
        {
            var userEvent = await _eventManager.GetEventByName(name);

            if (userEvent != null)
            {
                ViewBag.UserId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                var eventParticipantsDto = _eventManager.GetAllParticipants(userEvent.Id);
                var userNames = new List<string>();
                foreach (var userId in eventParticipantsDto)
                {
                    userNames.Add(await _accountManager.GetUserNameByIdAsync(userId.UserId));
                }
                ViewBag.ParticipantsNames = userNames;
                var eventViewModel = _mapper.Map<EventViewModel>(userEvent);
                return View(eventViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    model.UserId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                    model.AuthorName = User.Identity.Name;
                    model.PFreeNumber = model.PNumber; //TODO :Refactor it
                    var modelDto = _mapper.Map<EventDto>(model);
                    await _eventManager.CreateEventAsync(modelDto);
                    if (modelDto != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return NotFound(ExceptionResource.NotCreated);
                    }

                }
                return View(model);
            }
            else
            {
                throw new OtherException(ExceptionResource.NotCreated);
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(EventViewModel model)
        {
            if (model != null)
            {
                if (User.Identity.Name == model.AuthorName)
                {
                    var modelDto = _mapper.Map<EventDto>(model);
                    await _eventManager.DeleteEventAsync(modelDto);
                }
                else
                {
                    return NotFound(ModelErrorsResource.EventNotFound);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                throw new OtherException(ExceptionResource.NotDeleted);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditEventIndex(int id, string authorName)
        {
            if (User.Identity.Name == authorName)
            {
                var userEvent = await _eventManager.GetEventById(id);
                var eventViewModel = _mapper.Map<EventViewModel>(userEvent);
                return View(eventViewModel);
            }
            return NotFound(ModelErrorsResource.EventNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    var userEvent = await _eventManager.GetEventById(model.Id);
                    _mapper.Map<EventViewModel, EventDto>(model, userEvent);
                    await _eventManager.UpdateEventAsync(userEvent);
                }
                return RedirectToAction("Index", "Home");
            }
            throw new NotFoundException(ExceptionResource.EventNotFound);


        }

        ///TODO: Refactor if;
        [HttpPost]
        public async Task<IActionResult> AddParticipant(int eventId)
        {
            if (eventId != 0)
            {
                var eventDto = await _eventManager.GetEventById(eventId);
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                await _eventManager.AddParticipantAsync(eventId, userId, eventDto);
            }
            else
            {
                return NotFound(ModelErrorsResource.EventNotFound);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteParticipant(int eventId)
        {
            if (eventId != 0)
            {
                var eventDto = await _eventManager.GetEventById(eventId);
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                await _eventManager.DeleteParticipantAsync(eventId, userId, eventDto);
            }
            else
            {
                return NotFound(ModelErrorsResource.EventNotFound);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
