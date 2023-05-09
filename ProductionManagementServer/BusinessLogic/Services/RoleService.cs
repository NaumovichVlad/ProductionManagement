﻿using AutoMapper;
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

        public List<RoleDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(Role);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<RoleDto>>(_roleRepository.Get().OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<RoleDto>>(_roleRepository.Get().OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
        }

        public RoleDto GetRoleById(int id)
        {
            return _mapper.Map<RoleDto>(_roleRepository.GetById(id));
        }

    }
}
