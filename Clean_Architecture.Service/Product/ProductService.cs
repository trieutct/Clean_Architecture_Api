using AutoMapper;
using Clean_Architecture.Model.Dto.Category;
using Clean_Architecture.Model.Dto.Product;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Product> _repository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Clean_Architecture.Model.Entities.Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public bool Add(ProductDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.Product>(category));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return _mapper.Map<List<ProductDto>>(_repository.GetAll().OrderByDescending(x => x.ProductId));
        }

        public ProductDto GetById(int id)
        {
            return _mapper.Map<ProductDto>(_repository.GetbyId(id));
        }

        public IEnumerable<ProductDto> Search(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.Product>(category));
        }
    }
}
