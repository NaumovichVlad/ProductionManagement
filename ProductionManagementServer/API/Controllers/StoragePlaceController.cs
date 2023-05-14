using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("storagePlace/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class StoragePlaceController : ControllerBase
    {
        private readonly IStoragePlaceService _storagePlaceService;
        private readonly IMapper _mapper;
        public StoragePlaceController(IStoragePlaceService storagePlaceService, IMapper mapper)
        {
            _storagePlaceService = storagePlaceService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<StoragePlaceModel>>> GetStoragePlaces()
        {
            var items = _mapper.Map<List<StoragePlaceModel>>(_storagePlaceService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<StoragePlaceModel>>> GetStoragePlacesSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<StoragePlaceModel>>(_storagePlaceService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertStoragePlace([FromBody] StoragePlaceModel model)
        {
            _storagePlaceService.Insert(_mapper.Map<StoragePlaceDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditStoragePlace([FromBody] StoragePlaceModel model)
        {
            _storagePlaceService.Edit(_mapper.Map<StoragePlaceDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<StoragePlaceModel>> GetStoragePlaceById(int id)
        {
            var model = _mapper.Map<StoragePlaceModel>(_storagePlaceService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteStoragePlace(int id)
        {
            _storagePlaceService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
