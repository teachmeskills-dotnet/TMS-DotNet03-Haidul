using AutoMapper;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Web.Mappings
{
    public class EventWebProfile : Profile
    {
        public EventWebProfile()
        {
            CreateMap<EventViewModel, EventDto>().ReverseMap();
            CreateMap<EventDeleteViewModel, EventDto>().ReverseMap();
            CreateMap<EventDto, DAL.Entities.Event>().ReverseMap();
        }
    }
}
