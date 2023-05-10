﻿using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        bool CheckUser(UserDto userDto);
        UserDto GetUserByLogin(string login);
    }
}
