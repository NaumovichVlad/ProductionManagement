using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("salary/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        private readonly IMapper _mapper;
        public SalaryController(ISalaryService salaryService, IMapper mapper)
        {
            _salaryService = salaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<SalaryModel>>> GetSalaries()
        {
            var items = _mapper.Map<List<SalaryModel>>(_salaryService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<SalaryModel>>> GetSalariesSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<SalaryModel>>(_salaryService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertSalary([FromBody] SalaryModel model)
        {
            _salaryService.Insert(_mapper.Map<SalaryDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditSalary([FromBody] SalaryModel model)
        {
            _salaryService.Edit(_mapper.Map<SalaryDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<SalaryModel>> GetSalaryById(int id)
        {
            var model = _mapper.Map<SalaryModel>(_salaryService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            _salaryService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
