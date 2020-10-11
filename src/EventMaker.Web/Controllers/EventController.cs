using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
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

        public EventController(IAccountManager accountManager,
                              IEventManager eventManager)
        {
            _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
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
        public async Task<IActionResult> CreateEventAsync(EventViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var userId = await _accountManager.GetUserIdByNameAsync(User.Identity.Name);
            //    var eventDto = await _eventManager.CreateEventAsync(userId);
            //    if (eventDto.Succeeded)
            //    {
            //        return RedirectToAction("Event", "Index");
            //    }
            //    foreach (var error in eventDto.Errors)
            //    {
            //        ModelState.AddModelError(string.Empty, error.Description);
            //    }
            //}
            return View(model);
        }
    }
}
