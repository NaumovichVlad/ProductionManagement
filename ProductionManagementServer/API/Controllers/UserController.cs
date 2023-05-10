using API.Models;
using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
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
                Role = _roleService.GetById(u.RoleId).Name
            }).ToList();

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<UserModel>>> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<UserModel>>(_userService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }
    }
}
