using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class ProductService : Service<Product, ProductDto>, IProductService
    {
        public ProductService(IRepository<Product> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
