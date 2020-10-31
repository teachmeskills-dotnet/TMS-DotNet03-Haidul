using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Index(string name)
        {
            var userEvent = await _eventManager.GetEventByName(name);

            if (userEvent != null)
            {
                ViewBag.UserId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
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
            if (ModelState.IsValid)
            {
                model.UserId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                model.AuthorName = User.Identity.Name;
                var modelDto = _mapper.Map<EventDto>(model);
                await _eventManager.CreateEventAsync(modelDto);
                if (modelDto != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return NotFound("Event not created");/// TODO : rework this exceptions
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(EventViewModel model)
        {
            if (User.Identity.Name == model.AuthorName)
            {
                var modelDto = _mapper.Map<EventDto>(model);
                await _eventManager.DeleteEventAsync(modelDto);
            }
            else
            {
                return NotFound("Event not found"); /// TODO : rework this exceptions
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditEventIndex(string name, string authorName)
        {
            if (User.Identity.Name == authorName)
            {
                var userEvent = await _eventManager.GetEventByName(name);
                var eventViewModel = _mapper.Map<EventViewModel>(userEvent);
                ViewBag.ModelName = eventViewModel.Name;
                return View(eventViewModel);
            }
            return NotFound("Event not found"); /// TODO : rework this exceptions
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                var userEvent = await _eventManager.GetEventById(model.Id, userId);
                _mapper.Map<EventViewModel, EventDto>(model, userEvent);
                await _eventManager.EditEventAsync(userEvent);
            }
            else
            {
                return NotFound("Event not found"); /// TODO : rework this exceptions
            }
            return RedirectToAction("Index", "Home");
        }
    }
}