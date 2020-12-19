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
    /// <inheritdoc cref="IChatManager"/>
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

        public async Task<bool> SaveComment(int eventId, string userName, string message)
        {

            if ((string.IsNullOrWhiteSpace(message) != true) && userName != null)
            {
                if (!IsSpam(message))
                {
                    var comment = new Comment
                    {
                        EventId = eventId,
                        AuthorName = userName,
                        MessageText = message
                    };
                    await _repositoryComment.AddAsync(comment);
                    await _repositoryComment.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }
        public IEnumerable<CommentDto> GetAllEventComments(int eventId)
        {
            var comments = _repositoryComment.GetAllWithoutTracking().Where(ev => ev.EventId == eventId);
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return commentDto;
        }

        public async Task<bool> DeleteComment(int? eventId, string userName, string message)
        {
            if (eventId != null && userName != null)
            {
                var userComment = await _repositoryComment.GetEntityAsync(comment => comment.EventId == eventId && comment.AuthorName == userName && comment.MessageText == message);
                if (userComment != null)
                {
                    _repositoryComment.Delete(userComment);
                    await _repositoryComment.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> UpdateComment(int? eventId, string userName, string newMessage, string oldMessage)
        {
            if ((string.IsNullOrWhiteSpace(newMessage) != true) && eventId != null && userName != null)
            {
                var userComment = await _repositoryComment.GetEntityAsync(comment => comment.EventId == eventId
                                                                          && comment.AuthorName == userName
                                                                          && comment.MessageText == oldMessage);
                if (userComment != null && userComment.MessageText != newMessage)
                {
                    userComment.MessageText = newMessage;
                    _repositoryComment.Update(userComment);
                    await _repositoryComment.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        private bool IsSpam(string message)
        {
            var comments = _repositoryComment.GetAllWithoutTracking();
            foreach (var comment in comments)
            {
                if (comment.MessageText.ToLower() == message.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

    }
}
