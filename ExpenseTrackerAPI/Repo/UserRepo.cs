using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace ExpenseTrackerAPI.Repo
{
    public class UserRepo : UserRepoInterface
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepo(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<(int status, int userID)> CreateAccountAsync(UserAccount user)
        {
            using var connection = _databaseContext.getConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand("spCreateAccount", connection);
            command.CommandType = CommandType.StoredProcedure;

            //Setting parameter for stored procedure
            command.Parameters.AddWithValue("@user_email", user.Email);
            command.Parameters.AddWithValue("@user_name", user.Username);
            command.Parameters.AddWithValue("@user_password", user.HashedPassword);

            //Setting OUT parameter
            var statusParam = new MySqlParameter("@Status", MySqlDbType.Int32);
            var uidParam = new MySqlParameter("@uid", MySqlDbType.Int64);
            statusParam.Direction = ParameterDirection.Output;
            command.Parameters.Add(statusParam);
            command.Parameters.Add(uidParam);

            await command.ExecuteNonQueryAsync();
            int status = Convert.ToInt32(statusParam.Value);
            int uid = Convert.ToInt32(uidParam.Value);

            return (status, uid);
        }

        public async Task<(int status, int userID, string? username, string? hashedpassword)> UserLoginEmailAsync(string email)
        {
            using var connection = _databaseContext.getConnection();
            await connection.OpenAsync();

            using var command = new MySqlCommand("spLogin", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@user_email", email);

            var statusParam = new MySqlParameter("@status", MySqlDbType.Int32);
            var uidParam = new MySqlParameter("@uid", MySqlDbType.Int64);
            var usernameParam = new MySqlParameter("@user_name", MySqlDbType.VarChar, 255);
            var hashedpw = new MySqlParameter("@hashedpw", MySqlDbType.VarChar, 255);
            statusParam.Direction = ParameterDirection.Output;
            uidParam.Direction = ParameterDirection.Output;
            usernameParam.Direction = ParameterDirection.Output;
            hashedpw.Direction = ParameterDirection.Output;
            command.Parameters.Add(statusParam);
            command.Parameters.Add(uidParam);
            command.Parameters.Add(usernameParam);
            command.Parameters.Add(hashedpw);

            await command.ExecuteNonQueryAsync();
            int status = Convert.ToInt32(statusParam.Value);
            int userID = Convert.ToInt32(uidParam.Value);
            string? username = Convert.ToString(usernameParam.Value);
            string? hashedpassword = Convert.ToString(hashedpw.Value);

            return (status, userID, username, hashedpassword);
        }
    }
}
