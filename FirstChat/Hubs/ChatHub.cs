using FirstChat.Db;
using FirstChat.Models;
using Microsoft.AspNetCore.SignalR;

namespace FirstChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly DbConnect _connection;

        public ChatHub(DbConnect connection) => _connection = connection;

        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All.SendAsync("ReceiveMessage", "admin", $"{connection.userName} присоединился");
        }

        public async Task JoinLogChatRoom(UserConnection connection)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, connection.chatName);

            _connection.connections[Context.ConnectionId] = connection;

            await Clients.Group(connection.chatName).SendAsync("JoinLogChatRoom", "admin", $"{connection.userName} присоединился к чату {connection.chatName}!");
        }

        public async Task SendMessage(string message)
        {
            if(_connection.connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Group(connection.chatName).SendAsync("ReceiveServerMessage", connection.userName, message);
            }
        }
    }
}
