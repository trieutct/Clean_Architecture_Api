using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Model.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Product
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        IEnumerable<ProductDto> Search(string key);
        ProductDto GetById(int id);
        bool Add(ProductDto category);
        bool Update(ProductDto category);
        bool Delete(int id);
    }
}
