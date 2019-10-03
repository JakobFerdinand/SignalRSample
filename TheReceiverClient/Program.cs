using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static System.Console;

namespace TheReceiverClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/runnerHub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, TimeSpan>("RunnerCompletedMessage", (id, runningTime) =>
            {
                WriteLine($"Runner: {id} ran for {runningTime}");
            });

            await connection.StartAsync();
            WriteLine("Connection started.");

            ReadLine();
        }
    }
}
