using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    //DTO are use for request/response
    public class NewAccountDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include one uppercase letter, one number, and one special character.")]
        /*
         * ^(?=.*[A-Z])      # At least one uppercase letter
         * (?=.*\d)          # At least one digit
         * (?=.*[\W_])       # At least one special character (non-word)
         * .{8,}$            # Minimum 8 characters total
         */
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
