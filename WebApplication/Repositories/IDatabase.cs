using System.Data;

namespace WebApplication.Repositories
{
    public interface IDatabase 
    {
        DataTable ExecuteQuery(string query);
    }
}
