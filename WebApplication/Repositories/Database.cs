using Npgsql;
using System;
using System.Data;

namespace WebApplication.Repositories
{
    public class Database:IDatabase
    {
        private readonly string _connectionString;
        private readonly string _dbHost;
        private readonly string _dbPort;
        private readonly string _dbName;
        private readonly string _dbUser;
        private readonly string _dbPassword;

        public Database()
        {
            _dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            _dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            _dbName = Environment.GetEnvironmentVariable("DB_NAME");
            _dbUser = Environment.GetEnvironmentVariable("DB_USER");
            _dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            _connectionString = $"Host={_dbHost};Port={_dbPort};Database={_dbName};Username={_dbUser};Password={_dbPassword};";
        }
        public DataTable ExecuteQuery(string query) {
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, _connectionString);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
    }
}