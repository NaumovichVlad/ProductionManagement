using API.Models;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("user/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var items = _userService.GetList().Select(u => new UserModel
            {
                Login = u.Login,
                Password = u.Password,
                Role = _roleService.GetRoleById(u.RoleId).Name
            }).ToList();

            return await Task.FromResult(items);
        }
    }
}
