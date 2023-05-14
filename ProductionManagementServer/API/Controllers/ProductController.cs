using API.Models;
using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("product/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductOrderService _productOrderService;
        private readonly IProductsForOrderService _productsForOrderService;
        private readonly IProductsReserveService _productsReserveService;
        private readonly IFinishedProductService _finishedProductService;
        private readonly IFinishedProductForOrderService _finishedProductForOrderService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IProductOrderService productOrderService, 
            IProductsForOrderService productsForOrderService, IProductsReserveService productsReserveService, 
            IFinishedProductService finishedProductService, IFinishedProductForOrderService finishedProductForOrderService, 
            IMapper mapper)
        {
            _productService = productService;
            _productOrderService = productOrderService;
            _productsForOrderService = productsForOrderService;
            _productsReserveService = productsReserveService;
            _finishedProductService = finishedProductService;
            _finishedProductForOrderService = finishedProductForOrderService;
            _mapper = mapper;
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var items = _mapper.Map<List<ProductModel>>(_productService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("all/select")]
        public async Task<ActionResult<List<ProductModel>>> GetProductsSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<ProductModel>>(_productService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("insert")]
        public async Task<IActionResult> InsertProduct([FromBody] ProductModel model)
        {
            _productService.Insert(_mapper.Map<ProductDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("edit")]
        public async Task<IActionResult> EditProduct([FromBody] ProductModel model)
        {
            _productService.Edit(_mapper.Map<ProductDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("get")]
        public async Task<ActionResult<ProductModel>> GetProductById(int id)
        {
            var model = _mapper.Map<ProductModel>(_productService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("remove")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _productService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("order/all")]
        public async Task<ActionResult<IEnumerable<ProductOrderModel>>> GetProductOrders()
        {
            var items = _mapper.Map<List<ProductOrderModel>>(_productOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("order/all/select")]
        public async Task<ActionResult<List<ProductOrderModel>>> GetProductOrdersSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<ProductOrderModel>>(_productOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("order/insert")]
        public async Task<IActionResult> InsertProductOrder([FromBody] ProductOrderModel model)
        {
            _productOrderService.Insert(_mapper.Map<ProductOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("order/edit")]
        public async Task<IActionResult> EditProductOrder([FromBody] ProductOrderModel model)
        {
            _productOrderService.Edit(_mapper.Map<ProductOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("order/get")]
        public async Task<ActionResult<ProductOrderModel>> GetProductOrderById(int id)
        {
            var model = _mapper.Map<ProductOrderModel>(_productOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("order/remove")]
        public async Task<IActionResult> DeleteProductOrder(int id)
        {
            _productOrderService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("forOrder/all")]
        public async Task<ActionResult<IEnumerable<ProductsForOrdersModel>>> GetProductsForOrders()
        {
            var items = _mapper.Map<List<ProductsForOrdersModel>>(_productsForOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("forOrder/all/select")]
        public async Task<ActionResult<List<ProductsForOrdersModel>>> GetProductsForOrdersSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<ProductsForOrdersModel>>(_productsForOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("forOrder/insert")]
        public async Task<IActionResult> InsertProductsForOrder([FromBody] ProductsForOrdersModel model)
        {
            _productsForOrderService.Insert(_mapper.Map<ProductsForOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("forOrder/edit")]
        public async Task<IActionResult> EditProductsForOrder([FromBody] ProductsForOrdersModel model)
        {
            _productsForOrderService.Edit(_mapper.Map<ProductsForOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("forOrder/get")]
        public async Task<ActionResult<ProductsForOrdersModel>> GetProductsForOrderById(int id)
        {
            var model = _mapper.Map<ProductsForOrdersModel>(_productsForOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("forOrder/remove")]
        public async Task<IActionResult> DeleteProductsForOrder(int id)
        {
            _productsForOrderService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("reserve/all")]
        public async Task<ActionResult<IEnumerable<ProductsReserveModel>>> GetProductsReserves()
        {
            var items = _mapper.Map<List<ProductsReserveModel>>(_productsReserveService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("reserve/all/select")]
        public async Task<ActionResult<List<ProductsReserveModel>>> GetProductsReserveSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<ProductsReserveModel>>(_productsReserveService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("reserve/insert")]
        public async Task<IActionResult> InsertProductsReserve([FromBody] ProductsReserveModel model)
        {
            _productsReserveService.Insert(_mapper.Map<ProductsReserveDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("reserve/edit")]
        public async Task<IActionResult> EditProductsReserve([FromBody] ProductsReserveModel model)
        {
            _productsReserveService.Edit(_mapper.Map<ProductsReserveDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("reserve/get")]
        public async Task<ActionResult<ProductsReserveModel>> GetProductsReserveById(int id)
        {
            var model = _mapper.Map<ProductsReserveModel>(_productsReserveService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("reserve/remove")]
        public async Task<IActionResult> DeleteProductsReserve(int id)
        {
            _productsReserveService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("finished/all")]
        public async Task<ActionResult<IEnumerable<FinishedProductModel>>> GetFinishedProducts()
        {
            var items = _mapper.Map<List<FinishedProductModel>>(_finishedProductService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("finished/all/select")]
        public async Task<ActionResult<List<FinishedProductModel>>> GetFinishedProductsSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<FinishedProductModel>>(_finishedProductService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("finished/insert")]
        public async Task<IActionResult> InsertFinishedProduct([FromBody] FinishedProductModel model)
        {
            _finishedProductService.Insert(_mapper.Map<FinishedProductDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("finished/edit")]
        public async Task<IActionResult> EditFinishedProduct([FromBody] FinishedProductModel model)
        {
            _finishedProductService.Edit(_mapper.Map<FinishedProductDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("finished/get")]
        public async Task<ActionResult<FinishedProductModel>> GetFinishedProductById(int id)
        {
            var model = _mapper.Map<FinishedProductModel>(_finishedProductService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("finished/remove")]
        public async Task<IActionResult> DeleteFinishedProduct(int id)
        {
            _finishedProductService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet]
        [ActionName("finished/forOrder/all")]
        public async Task<ActionResult<IEnumerable<FinishedProductsForOrderModel>>> GetFinishedProductsForOrder()
        {
            var items = _mapper.Map<List<FinishedProductsForOrderModel>>(_finishedProductForOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("finished/forOrder/all/select")]
        public async Task<ActionResult<List<FinishedProductsForOrderModel>>> GetFinishedProductsForOrderSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<FinishedProductsForOrderModel>>(_finishedProductForOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("finished/forOrder/insert")]
        public async Task<IActionResult> InsertFinishedProductForOrder([FromBody] FinishedProductsForOrderModel model)
        {
            _finishedProductForOrderService.Insert(_mapper.Map<FinishedProductsForOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("finished/forOrder/edit")]
        public async Task<IActionResult> EditFinishedProductForOrder([FromBody] FinishedProductsForOrderModel model)
        {
            _finishedProductForOrderService.Edit(_mapper.Map<FinishedProductsForOrderDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("finished/forOrder/get")]
        public async Task<ActionResult<FinishedProductsForOrderModel>> GetFinishedProductForOrderById(int id)
        {
            var model = _mapper.Map<FinishedProductsForOrderModel>(_finishedProductForOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("finished/forOrder/remove")]
        public async Task<IActionResult> DeleteFinishedProductForOrder(int id)
        {
            _finishedProductForOrderService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }
    }
}
