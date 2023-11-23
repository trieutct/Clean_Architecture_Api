using AutoMapper;
using Clean_Architecture.Model.Dto.OrderDetail;
using Clean_Architecture.Model.Entities;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.OrderDetail
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.OrderDetail> _repository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Product> _Productrepository;
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.Order> _Orderrepository;
        private readonly IMapper _mapper;
        public OrderDetailService(IGenericRepository<Model.Entities.OrderDetail> repository, IMapper mapper, IGenericRepository<Model.Entities.Product> productrepository,
            IGenericRepository<Clean_Architecture.Model.Entities.Order> Orderrepository)
        {
            _repository = repository;
            _mapper = mapper;
            _Productrepository = productrepository;
            _Orderrepository = Orderrepository;
        }
        public bool Add(OrderDetailDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.OrderDetail>(category));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<OrderDetailDto> GetAll()
        {
            return _mapper.Map<List<OrderDetailDto>>(_repository.GetAll().OrderByDescending(x => x.Id));
        }

        public IEnumerable<OrderDetailDto> GetById(int OrderId)
        {
            var query = from OrderDetail in _repository.GetAll().Where(x=>x.OderId== OrderId).ToList()
                        join productbl in _Productrepository.GetAll().ToList() on OrderDetail.ProductId equals productbl.ProductId
                        select new OrderDetailDto
                        {
                            Id = OrderDetail.Id,
                            ProductId = OrderDetail.ProductId,
                            LinkAnh = productbl.ProductImage,
                            OderId = OrderDetail.OderId,
                            Price = OrderDetail.Price,
                            ProductName = productbl.ProductName,
                            Quantity = OrderDetail.Quantity
                        };
            return query.ToList();
            //return _mapper.Map<OrderDetailDto>(_repository.GetbyId(id));
        }

        public bool Update(OrderDetailDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.OrderDetail>(category));
        }
    }
}
