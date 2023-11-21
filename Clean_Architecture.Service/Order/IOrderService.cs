using Clean_Architecture.Model.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Order
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAll();
        IEnumerable<OrderDto> GetAllDonHangCanDuyet();
        OrderDto GetById(int id);
        bool Add(OrderDto OrderDto);
        bool Update(OrderDto OrderDto);
        bool Delete(int id);
    }
}
