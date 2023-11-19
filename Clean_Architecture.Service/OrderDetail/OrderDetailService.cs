using AutoMapper;
using Clean_Architecture.Model.Dto.OrderDetail;
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
        private readonly IMapper _mapper;
        public OrderDetailService(IGenericRepository<Model.Entities.OrderDetail> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

        public OrderDetailDto GetById(int id)
        {
            return _mapper.Map<OrderDetailDto>(_repository.GetbyId(id));
        }

        public bool Update(OrderDetailDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.OrderDetail>(category));
        }
    }
}
