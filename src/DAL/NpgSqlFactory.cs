using System.Data.Common;
using Npgsql;

namespace DAL
{
    public class NpgSqlFactory : IFactoryConnection
    {
        private readonly string _connectionString;

        public NpgSqlFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection GetDbConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}