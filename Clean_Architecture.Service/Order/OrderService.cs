using AutoMapper;
using Clean_Architecture.Model.Dto.Order;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.Order
{
    public class OrderService:IOrderService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Order> _repository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.OrderDetail> _OrderDetailrepository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Product> _productrepository;
        private readonly IMapper _mapper;
        public OrderService(IGenericRepository<Model.Entities.Order> repository,
            IMapper mapper,
            IGenericRepository<Model.Entities.OrderDetail> orderDetailrepository,
            IGenericRepository<Model.Entities.Product> productrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _OrderDetailrepository = orderDetailrepository;
            _productrepository = productrepository;
        }
        public bool Add(OrderDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.Order>(category));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return _mapper.Map<List<OrderDto>>(_repository.GetAll().OrderByDescending(x => x.Id));
        }
        public IEnumerable<OrderDto> GetAllDonHangCanDuyet()
        {
            return _mapper.Map<List<OrderDto>>(_repository.GetAll().Where(x=>x.TrangThai==0));
        }
        public OrderDto GetById(int id)
        {
            return _mapper.Map<OrderDto>(_repository.GetbyId(id));
        }

        public bool Update(OrderDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.Order>(category));
        }
        public IEnumerable<OrderDto> getOrderbyUserId(int userId)
        {
            return _mapper.Map<List<OrderDto>>(_repository.GetAll().Where(x => x.UserId == userId).OrderByDescending(x => x.UserId).ToList());
        }
    }
}
