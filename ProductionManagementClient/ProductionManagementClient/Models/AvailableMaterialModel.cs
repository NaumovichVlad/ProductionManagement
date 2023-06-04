using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class AvailableMaterialModel : ModelBase
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
