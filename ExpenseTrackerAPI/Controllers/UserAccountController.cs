using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repo;
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
        private readonly ExpenseCategoryRepo _categoryRepo;

        public UserAccountController(ExpenseCategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpPost("CreateExpenseCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] UserCategoryDto newCategory)
        {
            try
            {
                var (status, message) = await _categoryRepo.CreateCategoryAsync(newCategory.userId, newCategory.catergory_name.ToLower());
                return Created();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteExpenseCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] UserCategoryDto category)
        {
            try
            {
                int status = await _categoryRepo.DeleteCategoryAsync(category.userId, category.category_id);

                return NoContent();

            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("EditExpenseCategory")]
        public async Task<IActionResult> EditCategory([FromBody] UserCategoryDto category)
        {
            try
            {
                if (category.catergory_name == null)
                {
                    return BadRequest("Missing parameter");
                }
                int status = await _categoryRepo.EditCategoryAsync(category.userId, category.category_id, category.catergory_name);

                return Ok("Category updated");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}
