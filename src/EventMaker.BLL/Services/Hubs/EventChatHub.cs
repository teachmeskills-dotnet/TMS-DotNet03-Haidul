using System;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace EventMaker.BLL.Services
{
    public class EventChatHub : Hub
    {
        private readonly IChatManager _chatManager;


        public EventChatHub(IChatManager chatManager)
        {
            _chatManager = chatManager ?? throw new ArgumentNullException(nameof(chatManager));
        }

        public async Task SendMessage(string eventId, string userName, string message)
        {
            if ((string.IsNullOrWhiteSpace(message) != true) && eventId != null && userName != null)
            {
                var result = await _chatManager.SaveComment(Convert.ToInt32(eventId), userName, message);
                if (result)
                {
                    await Clients.All.SendAsync("ReceiveMessage", eventId, userName, message);
                }
                
            }

        }

        public async Task DeleteMessage(string eventId, string userName , string message)
        {
            if ((string.IsNullOrWhiteSpace(message) != true) && eventId != null && userName != null)
            {
                var result = await _chatManager.DeleteComment(Convert.ToInt32(eventId), userName , message);
                if (result)
                {
                    await Clients.All.SendAsync("DeleteMessage", eventId, userName, message);
                }              
            }
        }
        public async Task UpdateMessage(string eventId, string userName, string message)
        {
            if ((string.IsNullOrWhiteSpace(message) != true) &&  eventId != null && userName != null)
            {
                await Clients.All.SendAsync("EditMessage", eventId, userName, message);
                await _chatManager.UpdateComment(Convert.ToInt32(eventId), userName, message);
            }

        }
    }
}
