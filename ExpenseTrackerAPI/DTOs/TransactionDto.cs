using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ExpenseTrackerAPI.DTOs
{
    public class TransactionAddDTO
    {
        [Required]
        public int userID { get; set; }

        [Required]
        public int category_ID { get; set; }

        [Required] //need to format into 2 decimal place
        public decimal amount { get; set; }

        public string? description { get; set; }

        [Required]
        public required string date { get; set; }

    }

    public class TransactionDeleteDTO
    {
        [Required]
        public int userID { get; set; }

        [Required]
        public int transactionID { get; set; }

    }

    public class TransactionUpdateDTO
    {
        [Required]
        public int userID { get; set; }

        [Required]
        public int transactionID { get; set; }

        public int? category_ID { get; set; }

        public decimal? new_amount {  get; set; }

        public string? n_description { get; set; }

        public string? new_date { get; set; }

    }

}
