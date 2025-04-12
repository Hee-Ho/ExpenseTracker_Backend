using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ExpenseTrackerAPI.Database
{
    public class DatabaseContext : DbContext //Ctrl + . to install Nuget
    {
        private readonly string connection;

        public DatabaseContext(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("DefaultConnection");
        }

        public MySqlConnection getConnection()
        {
            return new MySqlConnection(connection);
        }
    }

}
