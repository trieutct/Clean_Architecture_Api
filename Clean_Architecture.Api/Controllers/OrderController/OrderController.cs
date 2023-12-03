using Clean_Architecture.Model.Dto.Order;
using Clean_Architecture.Model.Dto.OrderDetail;
using Clean_Architecture.Model.Entities;
using Clean_Architecture.Service.Cart;
using Clean_Architecture.Service.Order;
using Clean_Architecture.Service.OrderDetail;
using Clean_Architecture.Service.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.OrderController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetaiService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService, ICartService cartService)
        {
            _orderService = orderService;
            _orderDetaiService = orderDetailService;
            _productService=productService;
            _cartService = cartService;
        }
        [HttpPost]
        public IActionResult Add([FromBody] AddOrder model)
        {
            var oderDto = new OrderDto()
            {
                DiaChi = model.DiaChi,
                NguoiNhan = model.NguoiNhan,
                Phone = model.Phone,
                Total = model.Total,
                TrangThai = 0,
                NgayDat = DateTime.Now,
                UserId=model.UserId,
            };
            if (_orderService.Add(oderDto))
            {
                var findorder = _orderService.GetAll().Where(x => x.DiaChi.Equals(model.DiaChi) && x.NguoiNhan.Equals(model.NguoiNhan) && x.NgayDat == oderDto.NgayDat).FirstOrDefault();
                if (findorder == null)
                {
                    return BadRequest();
                }
                else
                {
                    foreach (var i in model.ListCartId)
                    {
                        var orderdetail = new OrderDetailDto()
                        {
                            OderId = findorder.Id,
                            Price = _productService.GetById(_cartService.GetById(i).ProductId).Price,
                            ProductId = _cartService.GetById(i).ProductId,
                            Quantity = _cartService.GetById(i).Quantity,
                        };
                        if (!_orderDetaiService.Add(orderdetail))
                            return BadRequest();
                        if(!_cartService.Delete(i))
                            return BadRequest();
                    }
                    return Ok("Đặt hàng thành công");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public IActionResult getOrderbyUserId(int id)
        {
            return Ok(_orderService.getOrderbyUserId(id));
        }
        [HttpGet("DaNhanDonHang")]
        public IActionResult confirmDaNhanDonHang(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
                return BadRequest();
            order.TrangThai = 3;
            if(_orderService.Update(order))
                return Ok();
            return BadRequest();
        }
    }
}
