using Clean_Architecture.Model.Dto.FavoriteProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.FavoriteProduct
{
    public interface IFavoriteProductService
    {
        IEnumerable<FavoriteProductDto> GetAll();
        FavoriteProductDto GetById(int id);
        bool Add(FavoriteProductDto favoriteProductDto);
        bool Update(FavoriteProductDto favoriteProductDto);
        bool Delete(int id);
    }
}
