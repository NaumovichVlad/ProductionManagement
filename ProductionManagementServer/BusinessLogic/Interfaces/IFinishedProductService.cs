﻿using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IFinishedProductService : IService<FinishedProductDto>
    {
        List<FinishedProductDto> GetNotAccepted();
        bool CreateFinishedProducts(int productId, int productCount);
    }
}
