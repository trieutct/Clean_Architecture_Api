using Clean_Architecture.Service.Dashboard;
using Clean_Architecture.Service.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Architecture.Api.Controllers.DashBoardController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService _DashboardService;
        public DashBoardController(IDashboardService iDashboardService)
        {
            _DashboardService = iDashboardService;
        }
        [HttpGet("getToatlAccountClient")]
        public IActionResult getAccountClient()
        {
            return Ok(_DashboardService.getAllAccountClient());
        }
        [HttpGet("getTotalProduct")]
        public IActionResult getTotalProduct()
        {
            return Ok(_DashboardService.getTotalProduct());
        }
        [HttpGet("getTotalOrder")]
        public IActionResult getTotalOrder()
        {
            return Ok(_DashboardService.getTotalOrder());
        }
        [HttpGet("getDonHangCanDuyet")]// khi status=0
        public IActionResult getDonHangCanDuyet()
        {
            return Ok(_DashboardService.getDonHangCanDuyet());
        }
        [HttpGet("getDonHangDanhChuanBi")]// khi status=1
        public IActionResult getDonHangDanhChuanBi()
        {
            return Ok(_DashboardService.getDonHangDangChuanBi());
        }
        [HttpGet("getDonHangDangGiao")]// khi status=2
        public IActionResult getDonHangDangGiao()
        {
            return Ok(_DashboardService.getDonHangDangGiao());
        }
        [HttpGet("getDonHangHoanThanh")]// khi status=3
        public IActionResult getDonHangHoanThanh()
        {
            return Ok(_DashboardService.getDonHangHoanThanh());
        }
        [HttpGet("getDonHangHuy")]// khi status=4
        public IActionResult getDonHangHuy()
        {
            return Ok(_DashboardService.getDonHangHuy());
        }
        [HttpPost("DuyetDonHang")]
        public IActionResult DuyetDonHang(List<int> listOrderId)
        {
            foreach(var i in listOrderId)
            {
                if (!_DashboardService.DuyetDonHang(i))
                    return BadRequest();
            }    
            return Ok("Duyệt thành công");
        }
        [HttpPost("setDonHangDangGiao")]
        public IActionResult setDonHangDangGiao(List<int> listOrderId)
        {
            foreach (var i in listOrderId)
            {
                if (!_DashboardService.setDonHangDangGiao(i))
                    return BadRequest();
            }
            return Ok("Thành công");
        }
        [HttpGet("DoanhThuTheoTuan")]
        public IActionResult getDoanhThuTheoTuan()
        {
            return Ok(_DashboardService.getDoanhThuTheoTuan());
        }
    }
}
