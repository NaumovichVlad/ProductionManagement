﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class SaleContainerDto
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CounteragentName { get; set; }
        public string Products { get; set; }
    }
}