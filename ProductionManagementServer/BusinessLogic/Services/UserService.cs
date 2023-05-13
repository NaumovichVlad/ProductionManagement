using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService : Service<User, UserDto>, IUserService
    {
        public UserService(IRepository<User> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }

        public bool CheckUser(UserDto userDto)
        {
            var users = _repository.Get(u => u.Login == userDto.Login);

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
            return _mapper.Map<UserDto>(_repository.Get(u => u.Login == login, null, "Role,Employee").First());
        }

        public new List<UserDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var type = typeof(User);
            var sortParameterProperty = type.GetProperty(sortParameter);
            if (sortDirection == "asc")
            {
                return _mapper.Map<List<UserDto>>(_repository.Get(null, null, "Role,Employee")
                    .OrderBy(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }
            else
            {
                return _mapper.Map<List<UserDto>>(_repository.Get(null, null, "Role,Employee")
                    .OrderByDescending(r => sortParameterProperty.GetValue(r)).Skip(start).Take(size).ToList());
            }

        }
    }
}
