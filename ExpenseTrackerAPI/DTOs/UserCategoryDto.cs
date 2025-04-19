using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        public int userId { get; set; }

        public string? catergory_name { get; set; }

    }

    public class DeleteCategoryDto
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int category_id { get; set; }
    }

    public class EditCategoryDto
    {
        [Required]
        public int userId { get; set; }

        [Required]
        public int category_id { get; set; }

        [Required]
        public string? new_categoryName { get; set; }
    }
}
