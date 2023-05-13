using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CounteragentService : Service<Counteragent, CounteragentDto>, ICounteragentService
    {
        public CounteragentService(IRepository<Counteragent> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
