using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using static System.Console;

namespace TheSenderClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8181/runnerHub")
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();
            WriteLine("Connection started.");

            var starttime = DateTime.Now;

            var input = string.Empty;
            while(true)
            {
                Write("Insert Runner Id: ");
                input = ReadLine();
                if (input is "exit")
                    return;

                var runningtime = DateTime.Now - starttime;

                await connection.SendAsync("RunnerCompleted", runningtime.ToString());
            }
        }
    }
}
