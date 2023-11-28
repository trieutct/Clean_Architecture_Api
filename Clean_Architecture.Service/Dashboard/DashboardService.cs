using Clean_Architecture.Model.Dto.Order;
using Clean_Architecture.Model.Entities;
using Clean_Architecture.Repository;
using Clean_Architecture.Service.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Dashboard
{
    public class DashboardService:IDashboardService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.AccountClient> _AccountClientRepository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Product> _productrepository;
        private readonly IOrderService _OrderService;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Order> _orderrepository;
        public DashboardService(IGenericRepository<Model.Entities.AccountClient> accountClientRepository,
            IGenericRepository<Model.Entities.Product> productrepository, 
            IGenericRepository<Clean_Architecture.Model.Entities.Order> orderrepository,
             IOrderService OrderService)
        {
            _AccountClientRepository = accountClientRepository;
            _productrepository= productrepository;
            _orderrepository= orderrepository;
            _OrderService= OrderService;
        }
        public long getAllAccountClient()
        {
            return _AccountClientRepository.GetAll().Count();
        }
        public long getTotalProduct()
        {
            return _productrepository.GetAll().Count();
        }
        public long getTotalOrder()
        {
            return _orderrepository.GetAll().Count();
        }
        public IEnumerable<OrderDto> getDonHangCanDuyet()
        {
            return _OrderService.GetAllDonHangCanDuyet();
        }
        public IEnumerable<OrderDto> getDonHangDangChuanBi()
        {
            return _OrderService.GetAllDonHangDangChuanBi();
        }
        public IEnumerable<OrderDto> getDonHangDangGiao()
        {
            return _OrderService.GetAllDonHangDangGiao();
        }
        public IEnumerable<OrderDto> getDonHangHoanThanh()
        {
            return _OrderService.GetAllDonHangHoanThanh();
        }
        public IEnumerable<OrderDto> getDonHangHuy()
        {
            return _OrderService.GetAllDonHangHuy();
        }
        public bool DuyetDonHang(int id)
        {
            var find=_OrderService.GetById(id);
            if (find == null)
                return false;
            find.TrangThai = 1;
            if(!_OrderService.Update(find)) return false;
            return true;
        }
        public bool setDonHangDangGiao(int id)
        {
            var find = _OrderService.GetById(id);
            if (find == null)
                return false;
            find.TrangThai = 2;
            if (!_OrderService.Update(find)) return false;
            return true;
        }
        public DoanhThuTheoTuan getDoanhThuTheoTuan()
        {
            DateTime currentDate = DateTime.Now;

            DateTime startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);

            DoanhThuTheoTuan doanhThuTuan = new DoanhThuTheoTuan();


            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = startOfWeek.AddDays(i);

                long totalRevenue = _OrderService.GetAll()
                    .Where(order => order.NgayDat.Date == currentDay.Date)
                    .Sum(order => order.Total);

                // Gán giá trị doanh thu cho từng ngày trong tuần
                switch (currentDay.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        doanhThuTuan.T2 = totalRevenue;
                        break;
                    case DayOfWeek.Tuesday:
                        doanhThuTuan.T3 = totalRevenue;
                        break;
                    case DayOfWeek.Wednesday:
                        doanhThuTuan.T4 = totalRevenue;
                        break;
                    case DayOfWeek.Thursday:
                        doanhThuTuan.T5 = totalRevenue;
                        break;
                    case DayOfWeek.Friday:
                        doanhThuTuan.T6 = totalRevenue;
                        break;
                    case DayOfWeek.Saturday:
                        doanhThuTuan.T7 = totalRevenue;
                        break;
                    case DayOfWeek.Sunday:
                        doanhThuTuan.CN = totalRevenue;
                        break;
                }
            }

            return doanhThuTuan;
        }
    }
}
