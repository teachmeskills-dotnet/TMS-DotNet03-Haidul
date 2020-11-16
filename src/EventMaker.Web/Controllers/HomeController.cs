using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index( int page = 1)
        {
            int pageSize = 4;

            var events = _eventManager.GetAllEvents();
            var count = events.Count();
            var takedEventDtos = events.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);

            var model = new HomeViewModel

            {
                EventDtos = takedEventDtos,
                PageViewModel = pageViewModel
            };

            return View(model);
        }
    }
}
