using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using Npgsql;

namespace Utils
{
    public static class MigrationData
    {
        public static async Task MigrationDataAsync(string connectionDbString, string pathJsonFile)
        {
            Console.WriteLine("Migration start");
            var data = GetData(pathJsonFile);
            await InsertToDbAsync(connectionDbString, data);
            Console.WriteLine("Migration completed");
        }

        private static string[] GetData(string pathJsonFile)
        {
            return JsonConvert.DeserializeObject<string[]>(File.ReadAllText(pathJsonFile));
        }

        private static async Task InsertToDbAsync(string connectionDbString, IEnumerable<string> data)
        {
            if(data == null || !data.Any()) return;

            using (var conn = new NpgsqlConnection(connectionDbString))
            {
                try
                {
                    await conn.OpenAsync();

                    const string query = @"INSERT INTO word_rus
                                          SELECT unnest(@Data)
                                          ON CONFLICT (word)
                                          DO NOTHING";
                    await conn.ExecuteAsync(query, new
                    {
                        Data = data
                    });
                }
                catch (PostgresException exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception);
                }
            }
        }
    }
}