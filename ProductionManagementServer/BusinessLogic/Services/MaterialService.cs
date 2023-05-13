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
    public class MaterialService : Service<Material, MaterialDto>, IMaterialService
    {
        public MaterialService(IRepository<Material> employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        { }
    }
}
