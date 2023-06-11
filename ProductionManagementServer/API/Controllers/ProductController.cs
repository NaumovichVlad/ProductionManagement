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
        private readonly ISaleService _productOrderService;
        private readonly IProductsReserveService _productsReserveService;
        private readonly IFinishedProductService _finishedProductService;
        private readonly IFinishedProductSaleService _finishedProductForOrderService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, ISaleService productOrderService, IProductsReserveService productsReserveService, 
            IFinishedProductService finishedProductService, IFinishedProductSaleService finishedProductForOrderService, 
            IMapper mapper)
        {
            _productService = productService;
            _productOrderService = productOrderService;
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
        [ActionName("sale/all")]
        public async Task<ActionResult<IEnumerable<SaleModel>>> GetProductOrders()
        {
            var items = _mapper.Map<List<SaleModel>>(_productOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("sale/all/select")]
        public async Task<ActionResult<List<SaleModel>>> GetProductOrdersSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<SaleModel>>(_productOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("sale/insert")]
        public async Task<IActionResult> InsertProductOrder([FromBody] SaleModel model)
        {
            _productOrderService.Insert(_mapper.Map<SaleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("sale/edit")]
        public async Task<IActionResult> EditProductOrder([FromBody] SaleModel model)
        {
            _productOrderService.Edit(_mapper.Map<SaleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("sale/get")]
        public async Task<ActionResult<SaleModel>> GetProductOrderById(int id)
        {
            var model = _mapper.Map<SaleModel>(_productOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpGet("{number}")]
        [ActionName("sale/getByNumber")]
        public async Task<ActionResult<SaleModel>> GetSaleByOrderNumber(string number)
        {
            var model = _mapper.Map<SaleModel>(_productOrderService.GetByOrderNumber(number));

            return new ObjectResult(model);
        }

        [HttpGet]
        [ActionName("sale/full")]
        public async Task<ActionResult<IEnumerable<SaleContainerModel>>> GetFullSales()
        {
            var items = _mapper.Map<List<SaleContainerModel>>(_productOrderService.GetFullSales());

            return await Task.FromResult(items);
        }

        [HttpDelete("{id}")]
        [ActionName("sale/remove")]
        public async Task<IActionResult> DeleteProductOrder(int id)
        {
            _productOrderService.Delete(id);

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

        [HttpGet("{id}")]
        [ActionName("reserve/place")]
        public async Task<ActionResult<List<ProductsReserveModel>>> GetMaterialsReserveByStoragePlaceId(int id)
        {
            var model = _mapper.Map<List<ProductsReserveModel>>(_productsReserveService.GetStorageReserves(id));

            return new ObjectResult(model);
        }

        [HttpGet]
        [ActionName("reserve/pending")]
        public async Task<ActionResult<List<FinishedProductModel>>> GetPendingMaterialsReserve()
        {
            var items = _mapper.Map<List<FinishedProductModel>>(_productsReserveService.GetPendingReserves());

            return await Task.FromResult(items);
        }

        [HttpGet]
        [ActionName("reserve/available/all")]
        public async Task<ActionResult<List<AvailableProductModel>>> GetAvailableProducts()
        {
            var model = _mapper.Map<List<AvailableProductModel>>(_productsReserveService.GetAvailableProducts());

            return new ObjectResult(model);
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

        [HttpPost("{productId}/{count}")]
        [ActionName("finished/create")]
        public async Task<IActionResult> CreateFinishedProduct(int productId, int count)
        {
            var message = string.Empty;

            if (_finishedProductService.CreateFinishedProducts(productId, count))
            {
                message = "Продукция успешно добавлена";
            }
            else
            {
                message = "Недостаточное количество материалов на складе";
            }

            return Ok(message);
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
        [ActionName("finished/notAccepted")]
        public async Task<ActionResult<IEnumerable<FinishedProductModel>>> GetNotAcceptedFinishedProducts()
        {
            var items = _mapper.Map<List<FinishedProductModel>>(_finishedProductService.GetNotAccepted());

            return await Task.FromResult(items);
        }

        [HttpGet]
        [ActionName("finished/sale/all")]
        public async Task<ActionResult<IEnumerable<FinishedProductSaleModel>>> GetFinishedProductsForOrder()
        {
            var items = _mapper.Map<List<FinishedProductSaleModel>>(_finishedProductForOrderService.GetList());

            return await Task.FromResult(items);
        }

        [HttpGet("{sortDirection}/{sortParameter}/{start}/{size}")]
        [ActionName("finished/sale/all/select")]
        public async Task<ActionResult<List<FinishedProductSaleModel>>> GetFinishedProductsForOrderSelection(int start, int size, string sortDirection, string sortParameter)
        {
            var roles = _mapper.Map<List<FinishedProductSaleModel>>(_finishedProductForOrderService.GetSelection(start, size, sortDirection, sortParameter));

            return new ObjectResult(roles);
        }

        [HttpPost]
        [ActionName("finished/sale/insert")]
        public async Task<IActionResult> InsertFinishedProductForOrder([FromBody] FinishedProductSaleModel model)
        {
            _finishedProductForOrderService.Insert(_mapper.Map<FinishedProductsSaleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPut]
        [ActionName("finished/sale/edit")]
        public async Task<IActionResult> EditFinishedProductForOrder([FromBody] FinishedProductSaleModel model)
        {
            _finishedProductForOrderService.Edit(_mapper.Map<FinishedProductsSaleDto>(model));

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpGet("{id}")]
        [ActionName("finished/sale/get")]
        public async Task<ActionResult<FinishedProductSaleModel>> GetFinishedProductForOrderById(int id)
        {
            var model = _mapper.Map<FinishedProductSaleModel>(_finishedProductForOrderService.GetById(id));

            return new ObjectResult(model);
        }

        [HttpDelete("{id}")]
        [ActionName("finished/sale/remove")]
        public async Task<IActionResult> DeleteFinishedProductForOrder(int id)
        {
            _finishedProductForOrderService.Delete(id);

            var response = Ok(new { Message = "Success" });

            return response;
        }

        [HttpPost("{name}/{count}/{saleId}")]
        [ActionName("finished/sale/insert/byProductName")]
        public async Task<IActionResult> InsertFinishedProductSaleByName(string name, int count, int saleId)
        {
            var message = string.Empty;

            if (_finishedProductForOrderService.InsertByName(name, count, saleId))
            {
                message = "Продукция успешно добавлена";
            }
            else
            {
                message = "Недостаточное количество продукции на складе";
            }

            return Ok(message);
        }
    }
}
