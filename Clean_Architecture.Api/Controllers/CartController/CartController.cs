using Clean_Architecture.Model.Dto.Cart;
using Clean_Architecture.Service.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.CartController
{
    [Authorize]
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
        public IActionResult Add([FromForm] AddCart data)
        {
            var find = _cartService.GetAll().Where(x => x.ProductId == data.ProductId && x.UserId == data.UserId).FirstOrDefault();
            if (find != null)
            {
                return Ok(new
                {
                    status = 0,
                    message = "Đã có trong giỏ hàng"
                });
            }
            var cartdto = new CartDto()
            {
                ProductId = data.ProductId,
                UserId = data.UserId,
                Quantity = data.Quantity,
            };
            if (_cartService.Add(cartdto))
            {
                return Ok(new
                {
                    status = 1,
                    message = "Thêm vào giỏ hàng thành công"
                });
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult getByUserId(int userId)
        {
            var listcart = _cartService.getCartsByUserId(userId);
            return Ok(listcart);
        }
        [HttpGet("UpdateCart")]
        public IActionResult TangGiamSoLuong(int userId, int productId, int status)
        {
            var findcart = _cartService.GetAll().Where(x => x.UserId == userId && x.ProductId == productId).FirstOrDefault();
            if (findcart == null)
                return BadRequest();
            if (status == 1)
                findcart.Quantity++;
            else
            {
                findcart.Quantity = (findcart.Quantity > 1) ? (findcart.Quantity - 1) : 1;
            }    
            if (_cartService.Update(findcart))
                return NoContent();
            return BadRequest();
        }

    }
}
