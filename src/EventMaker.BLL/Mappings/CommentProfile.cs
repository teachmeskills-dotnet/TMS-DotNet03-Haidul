using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;
using AutoMapperProfile = AutoMapper.Profile;

namespace EventMaker.BLL.Mappings
{
    /// <summary>
    /// Comment profile for Automapper.
    /// </summary>
    public class CommentProfile : AutoMapperProfile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap()
                .ForMember(comment => comment.Event, opt => opt.Ignore());
        }
    }
}
