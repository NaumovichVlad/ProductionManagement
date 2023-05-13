using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
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
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var items = _mapper.Map<List<UserModel>>(_userService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<UserModel>>> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<UserModel>>(_userService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> Insert([FromBody] UserModel model)
        {
            _userService.Insert(_mapper.Map<UserDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> Edit([FromBody] UserModel model)
        {
            _userService.Edit(_mapper.Map<UserDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var model = _mapper.Map<UserModel>(_userService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            _userService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
