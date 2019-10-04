using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TheServer.Models;
using TheServer.Services;

namespace TheServer.Hubs
{
    public class RunnerHub : Hub
    {
        private readonly IRunnerService runnerService;

        public RunnerHub(IRunnerService runnerService)
            => this.runnerService = runnerService;

        public async Task RunnerCompleted(string runningTime)
        {
            var runner = await runnerService.InsertOne(runningTime);
            var count = await runnerService.Count();
            Console.WriteLine($"Received: {runner.Id} - {runner.RunningTime}");
            Console.WriteLine($"Runners in db: {count}");
            await Clients.All.SendAsync("RunnerCompletedMessage", runner.Id, runner.RunningTime);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"ConnectionId: {Context.ConnectionId}");
            Console.WriteLine($"User: {Context.UserIdentifier}");
            return base.OnConnectedAsync();
        }
    }
}
