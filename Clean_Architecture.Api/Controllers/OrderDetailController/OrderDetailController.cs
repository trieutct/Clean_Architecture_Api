using Clean_Architecture.Service.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.OrderDetailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _OrderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _OrderDetailService = orderDetailService;
        }
        [HttpGet]
        public IActionResult getById(int orderId)
        {
            return Ok(_OrderDetailService.GetById(orderId));
        }
    }
}
