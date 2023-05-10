using API.Jwt;
using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("employee/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            var items = _mapper.Map<List<EmployeeModel>>(_employeeService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<EmployeeModel>>> GetSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<EmployeeModel>>(_employeeService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> Insert([FromBody] EmployeeModel model)
        {
            _employeeService.Insert(_mapper.Map<EmployeeDto>(model));

            var response = Ok(new { Message = "Success" });
            
            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> Edit([FromBody] EmployeeModel model)
        {
            _employeeService.Edit(_mapper.Map<EmployeeDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<RoleModel>> GetById(int id)
        {
            var model = _mapper.Map<EmployeeModel>(_employeeService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> Delete(int id)
        {
            _employeeService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
