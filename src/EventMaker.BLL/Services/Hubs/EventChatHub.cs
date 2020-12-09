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
            if (string.IsNullOrWhiteSpace(message) != true)
            {
                await Clients.All.SendAsync("ReceiveMessage", eventId, userName, message);
                await _chatManager.SaveComment(Convert.ToInt32(eventId), userName, message);
            }

        }
    }
}
