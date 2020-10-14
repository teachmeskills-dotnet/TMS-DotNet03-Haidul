using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using AutoMapper;
using Profile = AutoMapper.Profile;

namespace EventMaker.BLL.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDto, Event>().ReverseMap();
        }
    }


}
