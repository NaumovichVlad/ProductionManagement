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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository,  IMapper mapper, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public bool CheckUser(UserDto userDto)
        {
            var users = _userRepository.Get(u => u.Login == userDto.Login);

            if (users.Any())
            {
                var user = users.First();
                
                return user.Password == userDto.Password;
            }
            else
            {
                return false;
            }
        }

        public UserDto GetUserByLogin(string login)
        {
            return _mapper.Map<UserDto>(_userRepository.Get(u => u.Login == login).First());
        }

        public RoleDto GetRoleById(int id)
        {
            return _mapper.Map<RoleDto>(_roleRepository.GetById(id));
        }
    }
}
