using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace PRN222.Kahoot.Razor
{
    public class GameHub : Hub
    {
        public async Task JoinRoom(string roomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        }

        public async Task StartTimer(string roomCode, int duration)
        {
            int remainingTime = duration;
            while (remainingTime > 0)
            {
                await Clients.Group(roomCode).SendAsync("UpdateTimer", remainingTime);
                await Task.Delay(1000); // Chờ 1 giây
                remainingTime--;
            }
            await Clients.Group(roomCode).SendAsync("TimeUp");
        }
    }
}
