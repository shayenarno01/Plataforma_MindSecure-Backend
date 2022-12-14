using Backend_MindSecure.Models;
using Microsoft.AspNetCore.SignalR;

namespace Backend_MindSecure.Hubs
{
    public class ChatHub: Hub 
    {
        
        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("NewUser", $"{userName} entró al canal");
        }

        public async Task SendMessageAll(NewMessage message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message.UserName, message.Message);
        }

        // ////////////////////////////////////////////////////////////////////////

        public async Task ListGroup(string groupName, string userName)
        {
            await Clients.Group(groupName).SendAsync("NewUser", $"{userName} entró al canal");
        }

        ///////////////////////////////////////////////////

        public async Task LeaveGroup(string groupName, string userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("LeftUser", $"{userName} salió del canal");
        }

        public async Task SendMessage(NewMessage message)
        {
            await Clients.Group(message.GroupName).SendAsync("NewMessage", message);
        }

        //////////////////////////////////////////////////////

        public async Task SendMessage2(NewMessage message)
        {
            
            await Clients.User(message.UserName).SendAsync("NewMessage", message);
        }
    }

    public record NewMessage(string UserName, string Message, string GroupName);
    public record NewMessage2(string UserName, string Message);
}
