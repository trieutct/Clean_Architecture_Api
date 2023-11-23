using Clean_Architecture.Model.Dto.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.OrderDetail
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetailDto> GetAll();
        IEnumerable<OrderDetailDto> GetById(int id);
        bool Add(OrderDetailDto OrderDetailDto);
        bool Update(OrderDetailDto OrderDetailDto);
        bool Delete(int id);
    }
}
