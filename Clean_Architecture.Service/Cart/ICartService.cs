using Clean_Architecture.Model.Dto.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Cart
{
    public interface ICartService
    {
        IEnumerable<CartDetail> getCartsByUserId(int userId);
        IEnumerable<CartDto> GetAll();
        CartDto GetById(int id);
        bool Add(CartDto CartDto);
        bool Update(CartDto CartDto);
        bool Delete(int id);
    }
}
