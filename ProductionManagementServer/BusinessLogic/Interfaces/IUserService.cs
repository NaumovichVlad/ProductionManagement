using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        bool CheckUser(UserDto userDto);
        UserDto GetUserByLogin(string login);
        List<UserDto> GetList();
        List<UserDto> GetSelection(int start, int size, string sortDirection, string sortParameter);
    }
}
