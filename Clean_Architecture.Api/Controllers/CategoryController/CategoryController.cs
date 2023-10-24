using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Service.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("search")]
        public IActionResult SearchCategories(string key)
        {
            return Ok(_categoryService.Search(key));
        }
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoriesById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult PostCategory(CategoryDto category)
        {
            if (_categoryService.Add(category))
            {
                return CreatedAtAction("GetCategoriesById", new { id = category.CategoryId }, category);
            }
            return Ok("Danh mục sản phẩm đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult PutCategory(CategoryDto category)
        {
            if (_categoryService.Update(category))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_categoryService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì loại sản phẩm này không tồn tại");
        }
    }
}
