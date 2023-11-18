using AutoMapper;
using Clean_Architecture.Model.Dto.Cart;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Cart
{
    public class CartService:ICartService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Cart> _repository;
        private readonly IMapper _mapper;
        public CartService(IGenericRepository<Clean_Architecture.Model.Entities.Cart> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
       
        public bool Add(CartDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.Cart>(category));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<CartDto> GetAll()
        {
            return _mapper.Map<List<CartDto>>(_repository.GetAll().OrderByDescending(x => x.Id));
        }

        public CartDto GetById(int id)
        {
            return _mapper.Map<CartDto>(_repository.GetbyId(id));
        }

        public bool Update(CartDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.Cart>(category));
        }
    }
}
