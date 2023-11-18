using Clean_Architecture.Model.Dto.Cart;
using Clean_Architecture.Service.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.CartController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public IActionResult Add([FromForm]AddCart data)
        {
            var cartdto = new CartDto()
            {
                ProductId= data.ProductId,
                UserId= data.UserId,
                Quantity= data.Quantity,
            };
            if(_cartService.Add(cartdto))
            {
                return Ok("Thêm vào giỏ hàng thành công");
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult getByUserId(int userId)
        {

            return Ok();
        }
        
    }
}
