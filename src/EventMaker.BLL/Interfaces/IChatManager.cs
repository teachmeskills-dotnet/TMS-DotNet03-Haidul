using System.Collections.Generic;
using System.Threading.Tasks;
using EventMaker.BLL.Models;

namespace EventMaker.BLL.Interfaces
{
    /// <summary>
    /// Chat manager.
    /// </summary>
    public interface IChatManager
    {
        /// <summary>
        /// Save Comments.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="eventId">Event id.</param>
        /// <param name="userName">User name.</param>
        Task<bool> SaveComment(int eventId  , string message, string userName);

        /// <summary> 
        /// Get all comments.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <returns>IEnumerable<CommentDto></returns>
        IEnumerable<CommentDto> GetAllEventComments(int eventId);

        /// <summary>
        /// Delete comment.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <param name="userName">User name</param>
        /// <param name="message">User message</param>
        /// <returns></returns>
        Task<bool> DeleteComment(int? eventId, string userName , string message);

        /// <summary>
        /// Update comment.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <param name="userName">User name</param>
        /// <param name="message">User message</param>
        /// <returns></returns>
        Task UpdateComment(int? eventId, string userName , string message);
    }
}
