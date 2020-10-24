using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EventMaker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventManager _eventManager;
        private readonly IMapper _mapper;

        public HomeController(IEventManager eventManager,
                              IMapper mapper)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var events = _eventManager.GetAllEvents();
            var eventModels = _mapper.Map<IEnumerable<EventViewModel>>(events);

            return View(eventModels);
        }
    }
}
