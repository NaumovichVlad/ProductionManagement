using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class CounteragentService : Service<Counteragent, CounteragentDto>, ICounteragentService
    {
        public CounteragentService(IRepository<Counteragent> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
