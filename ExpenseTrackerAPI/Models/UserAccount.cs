using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Numerics;

namespace ExpenseTrackerAPI.Models
{
    public class UserAccount
    {

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? HashedPassword { get; set; }

    }
}
