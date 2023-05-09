using API.Models;
using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
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
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            var items = _mapper.Map<List<RoleModel>>(_roleService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<RoleModel>>> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<RoleModel>>(_roleService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }
    }
}
