using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace TheServer.Hubs
{
    public class RunnerHub : Hub
    {
        public async Task RunnerCompleted(string id, string runningTime)
        {
            Console.WriteLine($"Received: {id} - {runningTime}");
            await Clients.All.SendAsync("RunnerCompletedMessage", id, runningTime);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"ConnectionId: {Context.ConnectionId}");
            Console.WriteLine($"User: {Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }
    }
}
