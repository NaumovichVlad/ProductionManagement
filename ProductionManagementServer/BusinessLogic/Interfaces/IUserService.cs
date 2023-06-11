using BusinessLogic.Dtos;

namespace BusinessLogic.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        bool CheckUser(UserDto userDto);
        UserDto GetUserByLogin(string login);
    }
}
