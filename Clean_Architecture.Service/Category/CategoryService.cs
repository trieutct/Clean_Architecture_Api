using AutoMapper;
using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Model.Entities;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Category> _repository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Clean_Architecture.Model.Entities.Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IEnumerable<CategoryDto> Search(string key)
        {
            return _mapper.Map<List<CategoryDto>>(_repository.GetAll().OrderByDescending(x => x.CategoryId).Where(x=>x.CategoryName.Contains(key)));
        }
        public bool Add(CategoryDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.Category>(category));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<CategoryDto> GetAll()
        {
            return _mapper.Map<List<CategoryDto>>(_repository.GetAll().OrderByDescending(x => x.CategoryId));
        }

        public CategoryDto GetById(int id)
        {
            return _mapper.Map<CategoryDto>(_repository.GetbyId(id));
        }

        public bool Update(CategoryDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.Category>(category));
        }
    }
}
