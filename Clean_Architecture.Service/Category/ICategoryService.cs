using Clean_Architecture.Model.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Category
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAll();
        IEnumerable<CategoryDto> Search(string key);
        CategoryDto GetById(int id);
        bool Add(CategoryDto category);
        bool Update(CategoryDto category);
        bool Delete(int id);
    }
}
