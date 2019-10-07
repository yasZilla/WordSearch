using Microsoft.Extensions.Configuration;

namespace Utils
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = config.GetConnectionString("db");

            InitDatabase.Init(connectionString);
            MigrationData.MigrationDataAsync(connectionString, "word_rus.json").Wait();
        }
    }
}