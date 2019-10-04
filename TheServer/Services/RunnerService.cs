using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheServer.Models;

namespace TheServer.Services
{
    public interface IRunnerService
    {
        Task<long> Count();
        Task<IEnumerable<MongoRunner>> GetAll();
        Task<MongoRunner> InsertOne(string runningTime);
    }

    public class RunnerService : IRunnerService
    {
        private readonly IMongoCollection<MongoRunner> runners;
        
        public RunnerService(IRunnersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            runners = database.GetCollection<MongoRunner>(settings.RunnersCollectionName);
        }

        public Task<long> Count()
            => runners.CountDocumentsAsync(_ => true);

        public async Task<MongoRunner> InsertOne(string runningTime)
        {
            var runner = new MongoRunner
            {
                RunningTime = runningTime
            };
            await runners.InsertOneAsync(runner);
            return runner;
        }

        public async Task<IEnumerable<MongoRunner>> GetAll()
        {
            var all = await runners.FindAsync(_ => true);
            return await all.ToListAsync();
        }
    }
}
