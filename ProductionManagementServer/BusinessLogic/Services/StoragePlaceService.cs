using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class StoragePlaceService : Service<StoragePlace, StoragePlaceDto>, IStoragePlaceService
    {
        public StoragePlaceService(IRepository<StoragePlace> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
