using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Service.Category;
using Clean_Architecture.Service.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService categoryService)
        {
            _productService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] int page)
        {
            int begin;
            if (page <= 1)
            {
                begin = 0;
            }
            else
            {
                begin = (page * 10) - 10;
            }
            return Ok(_productService.GetAll().Skip(begin).Take(10));
            //return Ok(_categoryService.GetAll());
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody]ProductVM product)
        {
            //if (_productService.Add(category))
            //{
            //    return CreatedAtAction("GetCategoriesById", new { id = category.CategoryId }, category);
            //}
            return Ok("Danh mục sản phẩm đã tồn tại");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_productService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì loại sản phẩm này không tồn tại");
        }
    }
}
