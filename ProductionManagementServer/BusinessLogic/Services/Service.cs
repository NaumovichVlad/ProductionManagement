using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class Service<T, TDto> : IService<TDto> where T : class, IEntity
    {
        protected readonly IRepository<T> _repository;
        protected readonly IMapper _mapper;

        public Service(IRepository<T> employeeRepository, IMapper mapper)
        {
            _repository = employeeRepository;
            _mapper = mapper;
        }

        public List<TDto> GetList()
        {
            return _mapper.Map<List<TDto>>(_repository.Get()).ToList();
        }

        public List<TDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(T);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<TDto>>(_repository.Get().OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<TDto>>(_repository.Get().OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

        }

        public void Insert(TDto employeeDto)
        {
            _repository.Insert(_mapper.Map<T>(employeeDto));
        }

        public void Edit(TDto employeeDto)
        {
            _repository.Update(_mapper.Map<T>(employeeDto));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public TDto GetById(int id)
        {
            return _mapper.Map<TDto>(_repository.GetById(id));
        }
    }
}
