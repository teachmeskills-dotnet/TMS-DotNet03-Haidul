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

            CreateMap<EventViewModel, EventDto>()
                .ForMember(ev => ev.Created, opt => opt.Ignore())
                 .ForAllOtherMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}