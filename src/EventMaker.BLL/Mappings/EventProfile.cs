using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using AutoMapperProfile = AutoMapper.Profile;
using Profile = EventMaker.DAL.Entities.Profile;

namespace EventMaker.BLL.Mappings
{
    public class EventProfile : AutoMapperProfile
    {
        public EventProfile()
        {
            CreateMap<EventDto, Event>().ReverseMap();

            CreateMap<ProfileDto, Profile>().ReverseMap();

            CreateMap<EventParticipantDto, EventParticipant>().ReverseMap();
        }
    }
}
