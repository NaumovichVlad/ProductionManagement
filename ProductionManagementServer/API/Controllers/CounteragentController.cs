using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("counteragent/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CounteragentController : ControllerBase
    {
        private readonly ICounteragentService _counteragentService;
        private readonly IMapper _mapper;

        public CounteragentController(ICounteragentService counteragentService, IMapper mapper)
        {
            _counteragentService = counteragentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<CounteragentModel>>> GetCounteragents()
        {
            var items = _mapper.Map<List<CounteragentModel>>(_counteragentService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<CounteragentModel>>> GetCounteragentSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<CounteragentModel>>(_counteragentService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertCounteragent([FromBody] CounteragentModel model)
        {
            _counteragentService.Insert(_mapper.Map<CounteragentDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditCounteragent([FromBody] CounteragentModel model)
        {
            _counteragentService.Edit(_mapper.Map<CounteragentDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<CounteragentModel>> GetCounteragentById(int id)
        {
            var model = _mapper.Map<CounteragentModel>(_counteragentService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteCounteragent(int id)
        {
            _counteragentService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
