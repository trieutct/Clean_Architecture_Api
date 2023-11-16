using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Service.Category;
using Clean_Architecture.Service.Product;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clean_Architecture.Model.Entities;
using Clean_Architecture.Model.Dto.Product;
using Microsoft.AspNetCore.Authorization;

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
                begin = (page * 10) - 9;
            }
            return Ok(_productService.GetAll().Skip(begin).Take(9));
            //return Ok(_categoryService.GetAll());
        }
        [HttpGet("UserGetProduct")]
        public IActionResult UserGetProduct()
        {
            return Ok(_productService.GetAll().Skip(0).Take(12));
            //return Ok(_categoryService.GetAll());
        }
        [HttpPost]
        public IActionResult PostProduct([FromForm] ProductVM product)
        {
            var productdto = new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = long.Parse(product.Price),
                ProductImage = "",
                CategoryId = int.Parse(product.CategoryId),
            };
            var cloudinary = new Cloudinary(new Account("dbnr304ms", "337989117255642", "YPMNJzY1UjhFz20QC2_kr8mfqr0"));
            int productId = _productService.GetAll().ToList().Count == 0 ? 0 : _productService.GetAll().Max(x => x.ProductId);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(product.file.FileName, product.file.OpenReadStream()),
                PublicId = "ProductId_" + (productId + 1).ToString()
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100).Height(150).Crop("fill")).BuildUrl("olympic_flag");
            var imageUrl = uploadResult.SecureUrl.ToString();
            productdto.ProductImage = imageUrl;


            if (_productService.Add(productdto))
            {
                return CreatedAtAction("GetProductById", new { id = productdto.ProductId }, productdto);
            }
            return Ok("Danh mục sản phẩm đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult PutCategory([FromForm] ProductVM product)
        {
            var productdto = new ProductDto()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = long.Parse(product.Price),
                ProductImage = product.ProductImage,
                CategoryId = int.Parse(product.CategoryId),
            };
            if (product.file != null)
            {
                var cloudinary = new Cloudinary(new Account("dbnr304ms", "337989117255642", "YPMNJzY1UjhFz20QC2_kr8mfqr0"));
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(product.file.FileName, product.file.OpenReadStream()),
                    PublicId = "ProductId_" + (product.ProductId).ToString()
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.Transform(new Transformation().Width(100).Height(150).Crop("fill")).BuildUrl("olympic_flag");
                var imageUrl = uploadResult.SecureUrl.ToString();
                productdto.ProductImage = imageUrl;
            }
            if (_productService.Update(productdto))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var category = _productService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
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
