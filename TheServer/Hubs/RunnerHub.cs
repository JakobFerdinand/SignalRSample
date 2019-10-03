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
    }
}
