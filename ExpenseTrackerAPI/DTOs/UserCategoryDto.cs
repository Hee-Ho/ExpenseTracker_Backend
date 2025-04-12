using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class UserCategoryDto
    {
        [Required]
        public int userId { get; set; }

        public string? catergory_name { get; set; }

        public int category_id { get; set; }
    }
}
