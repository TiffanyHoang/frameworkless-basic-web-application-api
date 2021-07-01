using Npgsql;
using System;
using System.Data;

namespace WebApplication.Repositories
{
    public class Database : IDatabase
    {
        private readonly string _connectionString;
        public Database()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            _connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword};";
        }
        public DataTable ExecuteQuery(string query)
        {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, _connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}