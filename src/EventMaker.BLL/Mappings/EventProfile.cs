using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using AutoMapperProfile = AutoMapper.Profile;
using Profile = EventMaker.DAL.Entities.Profile;

namespace EventMaker.BLL.Mappings
{
    /// <summary>
    /// Event profile for Automapper.
    /// </summary>
    public class EventProfile : AutoMapperProfile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EventProfile()
        {
            CreateMap<EventDto, Event>().ReverseMap();

            CreateMap<ProfileDto, Profile>().ReverseMap();

            CreateMap<EventParticipantDto, EventParticipant>().ReverseMap();
        }
    }
}
