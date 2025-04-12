using ExpenseTrackerAPI.Database;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace ExpenseTrackerAPI.Repo
{
    public class ExpenseCategoryRepo
    {
        private readonly DatabaseContext _dbContext;

        public ExpenseCategoryRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(int status, string message)> CreateCategoryAsync (int userID, string category)
        {

            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spInsertExpenseCategory", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("uid", userID);
                command.Parameters.AddWithValue("category", category);

                var statusParam = new MySqlParameter("@status_code", MySqlDbType.Int32);
                statusParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(statusParam);

                await command.ExecuteNonQueryAsync();
                int status = Convert.ToInt32(statusParam.Value);

                return (status, "Success");
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.Message);           
            }
        }

        public async Task<int> DeleteCategoryAsync(int userID, int categoryID)
        {
            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spDeleteExpenseCategory", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("uid", userID);
                command.Parameters.AddWithValue("category_id", categoryID);

                var statusParam = new MySqlParameter("@status_code", MySqlDbType.Int32);
                statusParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(statusParam);

                await command.ExecuteNonQueryAsync();
                int status = Convert.ToInt32(statusParam.Value);

                return status;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> EditCategoryAsync(int userID, int categoryID, string new_name)
        {
            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spEditCategory", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("uid", userID);
                command.Parameters.AddWithValue("category_id", categoryID);
                command.Parameters.AddWithValue("new_name", new_name);

                var statusParam = new MySqlParameter("@status_code", MySqlDbType.Int32);
                statusParam.Direction = ParameterDirection.Output;
                command.Parameters.Add(statusParam);

                await command.ExecuteNonQueryAsync();
                int status = Convert.ToInt32(statusParam.Value);

                return status;
            }
            catch (MySqlException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }   
}
