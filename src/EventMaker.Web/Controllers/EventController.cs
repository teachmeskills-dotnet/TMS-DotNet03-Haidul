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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        //TODO : add human error handling
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                var modelDto = _mapper.Map<EventDto>(model);
                await _eventManager.CreateEventAsync(userId, modelDto);
                if (modelDto != null)
                {
                    return RedirectToAction("Home", "Index");
                }
                return NotFound("Event not created");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteEvent()
        {
            return View();
        }

        //TODO : add human error handling
        [HttpPost]
        public async Task<IActionResult> DeleteEvent(EventDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                var modelDto = _mapper.Map<EventDto>(model);
                await _eventManager.DeleteEventAsync(userId,modelDto);
                if (modelDto != null)
                {
                    return RedirectToAction("Event", "Index");
                }
                return NotFound("Event not created");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditEvent()
        {
            return View();
        }

        [HttpPost]
        //TODO : add human error handling
        public async Task<IActionResult> EditEvent(EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
                var modelDto = _mapper.Map<EventDto>(model);
                await _eventManager.EditEventAsync(userId, modelDto);
                if (modelDto != null)
                {
                    return RedirectToAction("Event", "Index");
                }
                return NotFound("Event not found");
            }
            return View(model);
        }
    }
}
