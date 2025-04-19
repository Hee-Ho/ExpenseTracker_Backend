using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.DTOs;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;
using System.Data;

namespace ExpenseTrackerAPI.Repo
{
    public class TransactionsRepo
    {
        private readonly DatabaseContext _dbContext;

        public TransactionsRepo(DatabaseContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<int> AddTransactionAsync(TransactionAddDTO transaction)
        {
            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spInsertTransaction", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("uid", transaction.userID);
                command.Parameters.AddWithValue("category_id", transaction.category_ID);
                command.Parameters.AddWithValue("transaction_amount", transaction.amount);
                command.Parameters.AddWithValue("descript", transaction.description);
                command.Parameters.AddWithValue("transaction_date", transaction.date);

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

        public async Task<int> DeleteTransactionAsync(TransactionDeleteDTO transaction)
        {
            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spDeleteTransaction", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("uid", transaction.userID);
                command.Parameters.AddWithValue("transaction_id", transaction.transactionID);

                var statusParam = new MySqlParameter("status_code", MySqlDbType.Int32);
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
        
        public async Task<int> UpdateTransactionAsync(TransactionUpdateDTO transaction)
        {
            try
            {
                using var connection = _dbContext.getConnection();
                await connection.OpenAsync();

                using var command = new MySqlCommand("spUpdateTransaction", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("uid", transaction.userID);
                command.Parameters.AddWithValue("transaction_id", transaction.transactionID);
                command.Parameters.AddWithValue("category_id", transaction.category_ID);
                command.Parameters.AddWithValue("new_amount", transaction.new_amount);
                command.Parameters.AddWithValue("new_descript", transaction.n_description);
                command.Parameters.AddWithValue("new_date", transaction.new_date);

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
