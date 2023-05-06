using API.Models;
using AutoMapper;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("role/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetSalaries()
        {
            var items = _mapper.Map<List<RoleModel>>(_roleService.GetList());

            return await Task.FromResult(items);
        }
    }
}
