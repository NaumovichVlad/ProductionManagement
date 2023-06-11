using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        protected readonly IRepository<User> _repository;
        protected readonly IMapper _mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _repository = userRepository;
            _mapper = mapper;
        }

        public bool CheckUser(UserDto userDto)
        {
            var users = _repository.Get(u => u.Login == userDto.Login);

            if (users.Any())
            {
                var user = users.First();

                return user.Password == PasswordHashService.GeneratePasswordHash(userDto.Password);
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

        public List<UserDto> GetSelection(int start, int size, string sortDirection, string sortParameter)
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

        public List<UserDto> GetList()
        {
            return _mapper.Map<List<UserDto>>(_repository.Get()).ToList();
        }

        public void Insert(UserDto userDto)
        {
            userDto.Password = PasswordHashService.GeneratePasswordHash(userDto.Password);
            _repository.Insert(_mapper.Map<User>(userDto));
        }

        public void Edit(UserDto userDto)
        {
            var user = _repository.GetById(userDto.Id);

            if (user.Password != userDto.Password)
            {
                userDto.Password = PasswordHashService.GeneratePasswordHash(userDto.Password);
            }

            _repository.Update(_mapper.Map<User>(userDto));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public UserDto GetById(int id)
        {
            return _mapper.Map<UserDto>(_repository.GetById(id));
        }
    }
}
