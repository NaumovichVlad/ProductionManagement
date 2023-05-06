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
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository,  IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public List<UserDto> GetList()
        {
            return _mapper.Map<List<UserDto>>(_userRepository.Get());
        }
    }
}
