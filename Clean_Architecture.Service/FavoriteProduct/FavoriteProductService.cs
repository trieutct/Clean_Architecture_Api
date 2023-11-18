using AutoMapper;
using Clean_Architecture.Model.Dto.FavoriteProduct;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.FavoriteProduct
{
    public class FavoriteProductService:IFavoriteProductService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.FavoriteProduct> _repository;
        private readonly IMapper _mapper;
        public FavoriteProductService(IGenericRepository<Clean_Architecture.Model.Entities.FavoriteProduct> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public bool Add(FavoriteProductDto category)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.FavoriteProduct>(category));
        }
        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<FavoriteProductDto> GetAll()
        {
            return _mapper.Map<List<FavoriteProductDto>>(_repository.GetAll().OrderByDescending(x => x.Id));
        }

        public FavoriteProductDto GetById(int id)
        {
            return _mapper.Map<FavoriteProductDto>(_repository.GetbyId(id));
        }

        public bool Update(FavoriteProductDto category)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.FavoriteProduct>(category));
        }
    }
}
