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

            CreateMap<ProfileViewModel, ProfileDto>()
            .ForMember(ev => ev.Created, opt => opt.Ignore())
            .ForMember(ev => ev.Image, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ProfileDto, ProfileViewModel>()
                .ForMember(ev => ev.Image, opt => opt.Ignore())
                .ForMember(ev => ev.ByteImage, opt => opt.MapFrom(ev => ev.Image));

            CreateMap<ProfileEditViewModel, ProfileDto>().ReverseMap();

            CreateMap<ProfileEditViewModel, ProfileDto>()
            .ForMember(ev => ev.Created, opt => opt.Ignore())
            .ForMember(ev => ev.Image, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EventViewModel, EventDto>()
                .ForMember(ev => ev.Created, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
