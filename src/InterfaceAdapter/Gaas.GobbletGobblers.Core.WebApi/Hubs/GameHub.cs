using Gaas.GobbletGobblers.Application;
using Microsoft.AspNetCore.SignalR;

namespace Gaas.GobbletGobblers.Core.WebApi.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinRoom(string gameId, string playerName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId, Context.ConnectionAborted);

            await Clients.Group(gameId).SendAsync("ReceiveMessage",
                new SendMessageModel
                {
                    PlayerName = playerName,
                    Message = $"{playerName} Join {gameId}",
                });
        }

        public async Task SendMessage(string gameId, string playerName, string message)
        {
            await Clients.Group(gameId).SendAsync("ReceiveMessage",
                new SendMessageModel
                {
                    PlayerName = playerName,
                    Message = message,
                });
        }
    }
}
