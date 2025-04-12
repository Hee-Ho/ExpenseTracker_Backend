using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class TransactionDTO
    {
        [Required]
        public int userID { get; set; }
    }

}
