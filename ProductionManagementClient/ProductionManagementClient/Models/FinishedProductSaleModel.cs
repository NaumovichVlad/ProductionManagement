﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class FinishedProductSaleModel : ModelBase
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductsReserveId { get; set; }
        public int Count { get; set; }
        public string SaleNumber { get; set; }
        public string ProductName { get; set; }
    }
}
