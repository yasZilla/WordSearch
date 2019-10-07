using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Support;

namespace Utils
{
    public static class InitDatabase
    {
        public static void Init(string connectionString)
        {
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var upgrade =
                DeployChanges.To
                    .PostgresqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith("Utils.Scripts.PreDeployment"), new SqlScriptOptions{ScriptType = ScriptType.RunAlways, RunGroupOrder = 0 })
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith("Utils.Scripts.Deployment"), new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), x => x.StartsWith("Utils.Scripts.PostDeployment"), new SqlScriptOptions { ScriptType = ScriptType.RunAlways, RunGroupOrder = 2 })
                    .LogToConsole()
                    .Build();

            var result = upgrade.PerformUpgrade();
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Success!");
            Console.ResetColor();
        }
    }
}