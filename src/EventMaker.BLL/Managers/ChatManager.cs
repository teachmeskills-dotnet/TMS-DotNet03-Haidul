using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Models;
using EventMaker.DAL.Entities;

namespace EventMaker.BLL.Managers
{
    public class ChatManager : IChatManager
    {
        private readonly IRepository<Comment> _repositoryComment;
        private readonly IMapper _mapper;

        public ChatManager(IRepository<Comment> repositoryComment,
                           IMapper mapper)
        {
            _repositoryComment = repositoryComment ?? throw new ArgumentNullException(nameof(repositoryComment));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task SaveComment(int eventId, string message, string userName)
        {
            if (message != null && userName != null)
            {
                var comment = new Comment
                {
                    EventId = eventId,
                    AuthorName = userName,
                    MessageText = message
                };
                await _repositoryComment.AddAsync(comment);
                await _repositoryComment.SaveChangesAsync();
            }
        }
        public IEnumerable<CommentDto> GetAllEventComments(int eventId)
        {
            var comments = _repositoryComment.GetAllWithoutTracking().Where(ev => ev.EventId == eventId);
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return commentDto;
        }

    }
}
