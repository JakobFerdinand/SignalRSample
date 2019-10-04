namespace TheServer
{
    public interface IRunnersDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string RunnersCollectionName { get; set; }
    }

    public class RunnersDatabaseSettings : IRunnersDatabaseSettings
    {
        public string RunnersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
