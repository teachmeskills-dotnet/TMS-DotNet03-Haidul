using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using AutoMapperProfile = AutoMapper.Profile;


namespace EventMaker.BLL.Mappings
{
    public class CommentProfile : AutoMapperProfile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap()
                .ForMember(comment => comment.Event, opt => opt.Ignore());
        }
    }
}
