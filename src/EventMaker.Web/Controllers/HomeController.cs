using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.BLL.Services;
using EventMaker.Common.Enums;
using EventMaker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EventMaker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventManager _eventManager;
        private readonly IMapper _mapper;
        private readonly IFiltrationService _filtrationService;

        public HomeController(IEventManager eventManager,
                              IMapper mapper, IFiltrationService filtrationService)
        {
            _eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _filtrationService = filtrationService ?? throw new ArgumentNullException(nameof(filtrationService));
        }

        [HttpGet]
        public IActionResult Index(FilterOptions filterOptions , EventFormats eventFormats , int page = 1 , string name = null)
        {
            int pageSize = 4;

            var events = _eventManager.GetAllEvents();
            if (!string.IsNullOrEmpty(name))
            {
                events = _filtrationService.FilterEvents(filterOptions, eventFormats , events , name);
            }
            var count = events.Count();
            var takedEventDtos = events.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new HomeViewModel

            {
                EventDtos = takedEventDtos,
                PageViewModel = new PageViewModel(count, page, pageSize),
                FilterViewModel = new FilterViewModel(filterOptions, eventFormats , name),
            };

            return View(model);
        }
    }
}
