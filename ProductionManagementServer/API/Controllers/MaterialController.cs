using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("material/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IMaterialOrderService _materialOrderService;
        private readonly IMaterialsReserveService _materialsReserveService;
        private readonly IMaterialsForFinishedProductsService _materialsForFinishedProductsService;
        private readonly IMaterialsForProductsService _materialsForProductsService;
        private readonly IMapper _mapper;

        public MaterialController(IMaterialService materialService, IMaterialOrderService materialOrderService,
            IMaterialsReserveService materialsReserveService, IMaterialsForFinishedProductsService materialsForFinishedProductsService,
            IMaterialsForProductsService materialsForProductsService,IMapper mapper)
        {
            _materialService = materialService;
            _materialOrderService = materialOrderService;
            _materialsReserveService = materialsReserveService;
            _materialsForFinishedProductsService = materialsForFinishedProductsService;
            _materialsForProductsService = materialsForProductsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> GetMaterials()
        {
            var items = _mapper.Map<List<MaterialModel>>(_materialService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<MaterialModel>>> GetMaterialsSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<MaterialModel>>(_materialService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertMaterial([FromBody] MaterialModel model)
        {
            _materialService.Insert(_mapper.Map<MaterialDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditMaterial([FromBody] MaterialModel model)
        {
            _materialService.Edit(_mapper.Map<MaterialDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<MaterialModel>> GetMaterialById(int id)
        {
            var model = _mapper.Map<MaterialModel>(_materialService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            _materialService.Delete(id);


            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("order/all")]
        public async Task<ActionResult<IEnumerable<MaterialOrderModel>>> GetMaterialOrders()
        {
            var items = _mapper.Map<List<MaterialOrderModel>>(_materialOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("order/all/select")]
        public async Task<ActionResult<List<MaterialOrderModel>>> GetMaterialOrdersSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<MaterialOrderModel>>(_materialOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("order/insert")]
        public async Task<IActionResult> InsertMaterialsOrder([FromBody] MaterialOrderModel model)
        {
            _materialOrderService.Insert(_mapper.Map<MaterialOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("order/edit")]
        public async Task<IActionResult> EditMaterialsOrder([FromBody] MaterialOrderModel model)
        {
            _materialOrderService.Edit(_mapper.Map<MaterialOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("order/get")]
        public async Task<ActionResult<MaterialOrderModel>> GetMaterialsOrderById(int id)
        {
            var model = _mapper.Map<MaterialOrderModel>(_materialOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("order/remove")]
        public async Task<IActionResult> DeleteMaterialsOrder(int id)
        {
            _materialOrderService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("reserve/all")]
        public async Task<ActionResult<IEnumerable<MaterialReserveModel>>> GetMaterialReserves()
        {
            var items = _mapper.Map<List<MaterialReserveModel>>(_materialsReserveService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("reserve/all/select")]
        public async Task<ActionResult<List<MaterialReserveModel>>> GetMaterialReservesSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<MaterialReserveModel>>(_materialsReserveService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("reserve/insert")]
        public async Task<IActionResult> InsertMaterialsReserves([FromBody] MaterialReserveModel model)
        {
            _materialsReserveService.Insert(_mapper.Map<MaterialReserveDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("reserve/edit")]
        public async Task<IActionResult> EditMaterialsReserves([FromBody] MaterialReserveModel model)
        {
            _materialsReserveService.Edit(_mapper.Map<MaterialReserveDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("reserve/get")]
        public async Task<ActionResult<MaterialReserveModel>> GetMaterialsReserveById(int id)
        {
            var model = _mapper.Map<MaterialReserveModel>(_materialsReserveService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpGet("{id}")]
        [ActionName("reserve/get/available")]
        public async Task<ActionResult<List<MaterialReserveModel>>> GetAvailableMaterialsByMaterialId(int id)
        {
            var model = _mapper.Map<List<MaterialReserveModel>>(_materialsReserveService.GetAvailableReservesByMaterialId(id));

            return new ObjectResult(model);
        }

        [HttpGet("{id}")]
        [ActionName("reserve/get/consumption")]
        public async Task<ActionResult<List<MaterialReserveModel>>> GetConsumptionMaterialsByMaterialId(int id)
        {
            var model = _mapper.Map<List<MaterialReserveModel>>(_materialsReserveService.GetConsumptionReservesByMaterialId(id));

            return new ObjectResult(model);
        }

        [HttpGet("{id}")]
        [ActionName("reserve/place")]
        public async Task<ActionResult<List<MaterialReserveModel>>> GetMaterialsReserveByStoragePlaceId(int id)
        {
            var model = _mapper.Map<List<MaterialReserveModel>>(_materialsReserveService.GetStorageReserves(id));

            return new ObjectResult(model);
        }

        [HttpGet]
        [ActionName("reserve/pending")]
        public async Task<ActionResult<List<MaterialOrderModel>>> GetPendingMaterialsReserve()
        {
            var items = _mapper.Map<List<MaterialOrderModel>>(_materialsReserveService.GetPendingReserves());

            return await Task.FromResult(items);
        }

        [HttpDelete("{id}")]
        [ActionName("reserve/remove")]
        public async Task<IActionResult> DeleteMaterialReserves(int id)
        {
            _materialsReserveService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("forFinishedProducts/all")]
        public async Task<ActionResult<IEnumerable<MaterialsForFinishedProductsModel>>> GetMaterialForFinishedProducts()
        {
            var items = _mapper.Map<List<MaterialsForFinishedProductsModel>>(_materialsForFinishedProductsService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("forFinishedProducts/all/select")]
        public async Task<ActionResult<List<MaterialsForFinishedProductsModel>>> GetMaterialForFinishedProductsSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<MaterialsForFinishedProductsModel>>(_materialsForFinishedProductsService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("forFinishedProducts/insert")]
        public async Task<IActionResult> InsertMaterialsForFinishedProducts([FromBody] MaterialsForFinishedProductsModel model)
        {
            _materialsForFinishedProductsService.Insert(_mapper.Map<MaterialsForFinishedProductsDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("forFinishedProducts/edit")]
        public async Task<IActionResult> EditMaterialsForFinishedProducts([FromBody] MaterialsForFinishedProductsModel model)
        {
            _materialsForFinishedProductsService.Edit(_mapper.Map<MaterialsForFinishedProductsDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("forFinishedProducts/get")]
        public async Task<ActionResult<MaterialsForFinishedProductsModel>> GetMaterialsForFinishedProductsById(int id)
        {
            var model = _mapper.Map<MaterialsForFinishedProductsModel>(_materialsForFinishedProductsService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("forFinishedProducts/remove")]
        public async Task<IActionResult> DeleteMaterialForFinishedProducts(int id)
        {
            _materialsForFinishedProductsService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("forProducts/all")]
        public async Task<ActionResult<IEnumerable<MaterialsForProductsModel>>> GetMaterialForProducts()
        {
            var items = _mapper.Map<List<MaterialsForProductsModel>>(_materialsForProductsService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("forProducts/all/select")]
        public async Task<ActionResult<List<MaterialsForProductsModel>>> GetMaterialForProductsSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<MaterialsForProductsModel>>(_materialsForProductsService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("forProducts/insert")]
        public async Task<IActionResult> InsertMaterialsForProducts([FromBody] MaterialsForProductsModel model)
        {
            _materialsForProductsService.Insert(_mapper.Map<MaterialsForProductsDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("forProducts/edit")]
        public async Task<IActionResult> EditMaterialsForProducts([FromBody] MaterialsForProductsModel model)
        {
            _materialsForProductsService.Edit(_mapper.Map<MaterialsForProductsDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("forProducts/get")]
        public async Task<ActionResult<MaterialsForProductsModel>> GetMaterialsForProductsById(int id)
        {
            var model = _mapper.Map<MaterialsForProductsModel>(_materialsForProductsService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("forProducts/remove")]
        public async Task<IActionResult> DeleteMaterialForProducts(int id)
        {
            _materialsForProductsService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
