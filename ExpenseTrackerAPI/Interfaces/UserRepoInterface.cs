using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    //Not necessary in this case 
    public interface UserRepoInterface
    {
        Task<(int status, int userID)> CreateAccountAsync(UserAccount user);

        Task<(int status, int userID, string? username, string? hashedpassword)> UserLoginEmailAsync(string email);
    }
}
