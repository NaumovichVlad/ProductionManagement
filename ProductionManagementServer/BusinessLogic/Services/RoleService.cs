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
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public List<RoleDto> GetList()
        {
            return _mapper.Map<List<RoleDto>>(_roleRepository.Get());
        }

        public RoleDto GetRoleById(int id)
        {
            return _mapper.Map<RoleDto>(_roleRepository.GetById(id));
        }
    }
}
