using AutoMapper;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;

namespace EventMaker.Web.Mappings
{
    public class EventWebProfile : Profile
    {
        public EventWebProfile()
        {
            CreateMap<EventViewModel, EventDto>().ReverseMap();
            CreateMap<EventDeleteViewModel, EventDto>().ReverseMap();
        }
    }
}
