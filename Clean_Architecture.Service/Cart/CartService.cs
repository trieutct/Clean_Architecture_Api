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
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.AccountClient> _accountClientRepository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Product> _productRepository;
        private readonly IMapper _mapper;
        public CartService(
            IGenericRepository<Clean_Architecture.Model.Entities.Cart> repository,
            IGenericRepository<Clean_Architecture.Model.Entities.AccountClient> accountClient,
            IGenericRepository<Clean_Architecture.Model.Entities.Product> product,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountClientRepository= accountClient;
            _productRepository = product;
        }
        public IEnumerable<CartDetail> getCartsByUserId(int userId)
        {
            var query = from AccountClienttbl in _accountClientRepository.GetAll().Where(x => x.Id == userId).ToList()
                        join Carttbl in _repository.GetAll().Where(x => x.UserId == userId).ToList() on AccountClienttbl.Id equals Carttbl.UserId
                        join Producttbl in _productRepository.GetAll() on Carttbl.ProductId equals Producttbl.ProductId
                        select new CartDetail
                        {
                            Image=Producttbl.ProductImage,
                            Price=Producttbl.Price,
                            ProductId=Producttbl.ProductId,
                            ProductName=Producttbl.ProductName,
                            Quantity= Carttbl.Quantity,
                            UserId=userId,
                            CartId= Carttbl.Id
                        };
            return query.ToList();
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
