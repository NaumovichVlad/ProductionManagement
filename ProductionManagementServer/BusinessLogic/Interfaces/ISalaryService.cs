﻿using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ISalaryService
    {
        List<SalaryDto> GetList();
        List<SalaryDto> GetByEmployee(int employeeId);
        List<SalaryDto> GetByDate(DateTime startDate, DateTime endDate, Employee employee);
    }
}
