using API.Jwt;
using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("login/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public LoginController(IConfiguration config, IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _config = config;
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        private async Task<bool> AuthenticateUser(UserModel login)
        {
            return _userService.CheckUser(_mapper.Map<UserDto>(login));
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("autorize")]
        public async Task<IActionResult> Autorize([FromBody] UserModel model)
        {
            IActionResult response = Unauthorized();

            if (AuthenticateUser(model).Result)
            {

                var tokenString = JwtManager.GenerateJSONWebToken(_config);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }

        [HttpGet("{login}")]
        [ActionName("user")]
        public async Task<ActionResult<RoleModel>> GetUserByLogin(string login)
        {
            var user = _userService.GetUserByLogin(login);
            var userModel = _mapper.Map<UserModel>(user);
            userModel.RoleName = _roleService.GetById(user.RoleId).Name;

            return new ObjectResult(userModel);
        }
    }
}
