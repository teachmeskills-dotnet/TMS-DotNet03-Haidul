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

            CreateMap<ProfileViewModel, ProfileDto>().ReverseMap();

            CreateMap<ProfileEditViewModel, ProfileDto>().ReverseMap();

            CreateMap<ProfileViewModel , ProfileDto>()
            .ForMember(ev => ev.Created, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProfileEditViewModel, ProfileDto>()
            .ForMember(ev => ev.Created, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EventViewModel, EventDto>()
                .ForMember(ev => ev.Created, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}