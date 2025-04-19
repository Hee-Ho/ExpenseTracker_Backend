using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Repo;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/User/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ExpenseCategoryRepo _categoryRepo;

        public CategoryController(ExpenseCategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        //-----------------------------------------------Category-----------------------------------------------------------------------------
       
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto newCategory)
        {
            try
            {
                if (newCategory.catergory_name == null)
                {
                    return BadRequest("Parameter missing");
                }
                var (status, message) = await _categoryRepo.CreateCategoryAsync(newCategory.userId, newCategory.catergory_name.ToLower());
                return Created();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryDto category)
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

        [HttpPost("update")]
        public async Task<IActionResult> EditCategory([FromBody] EditCategoryDto category)
        {
            try
            {
                if (category.new_categoryName == null)
                {
                    return BadRequest("Missing parameter");
                }
                int status = await _categoryRepo.EditCategoryAsync(category.userId, category.category_id, category.new_categoryName);

                return Ok("Category updated");
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------
    }


}
