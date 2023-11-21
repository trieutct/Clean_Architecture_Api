using Clean_Architecture.Model.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Dashboard
{
    public interface IDashboardService
    {
        public long getAllAccountClient(); 
        public long getTotalProduct();
        public long getTotalOrder();
        public IEnumerable<OrderDto> getDonHangCanDuyet();
        public bool DuyetDonHang(int id);
        public DoanhThuTheoTuan getDoanhThuTheoTuan();

    }
}
