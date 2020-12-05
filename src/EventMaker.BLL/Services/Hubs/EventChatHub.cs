using System;
using System.Threading.Tasks;
using EventMaker.BLL.Interfaces;
using EventMaker.DAL.Entities;
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

        public async Task SendMessage(string eventId , string userName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", userName, message);
            if(string.IsNullOrWhiteSpace(message) != true)
            {
                await _chatManager.SaveEventComment(Convert.ToInt32(eventId), userName, message);
            }
        
        }
    }
}
