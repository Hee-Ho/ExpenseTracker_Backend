using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Numerics;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        //load database connection
        private readonly DatabaseContext _databaseContext;

        public UserAccountController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet(Name = "GetUsersInfo")] //the request method to be use
        // Task<ActionResult<List<UserAccount>>> - use to define the result of an action/method
        // IActionResult includes everything
        public async Task<IActionResult> GetInfo()
        {
            List<string> userAccounts = new List<string>();

            using (var connection = _databaseContext.getConnection()) 
            { 
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT username FROM user_accounts", connection))
                using (var reader = command.ExecuteReader())
                { 
                    while (await reader.ReadAsync())
                    {
                        userAccounts.Add(reader.GetString(0));
                    }      
                }
            }
            return Ok(userAccounts);
        }

    }
}
