using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
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
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            var items = _mapper.Map<List<RoleModel>>(_roleService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<RoleModel>>> GetRoleSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<RoleModel>>(_roleService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertRole([FromBody] RoleModel model)
        {
            _roleService.Insert(_mapper.Map<RoleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditRole([FromBody] RoleModel model)
        {
            _roleService.Edit(_mapper.Map<RoleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<RoleModel>> GetRoleById(int id)
        {
            var model = _mapper.Map<RoleModel>(_roleService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _roleService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
