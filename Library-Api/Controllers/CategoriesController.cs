using Application.DTO.Category;
using Application.UseCases.Category;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.ListCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryDetails(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var category = await _categoryService.GetCategoryDetails(name);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCDto categoryCDto)
        {
            int newCategoryid = await _categoryService.CreateCategory(categoryCDto);
            return CreatedAtAction(nameof(GetById), new { id = newCategoryid }, categoryCDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUDto categoryUDto)
        {
            if (id != categoryUDto.ID)
                return BadRequest();

            await _categoryService.UpdateCategory(categoryUDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
