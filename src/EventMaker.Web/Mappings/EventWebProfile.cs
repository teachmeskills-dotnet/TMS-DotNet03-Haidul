using AutoMapper;
using EventMaker.BLL.Models;
using EventMaker.Web.ViewModels;
using System.Threading.Tasks;

namespace EventMaker.Web.Mappings
{
    public class EventWebProfile : Profile
    {
        public EventWebProfile()
        {
            CreateMap<EventViewModel, EventDto>().ReverseMap();

            CreateMap<EventViewModel, EventDto>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
