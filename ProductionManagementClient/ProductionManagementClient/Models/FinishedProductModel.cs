using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class FinishedProductModel : ModelBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ProductName { get; set; }
        public bool IsApproved { get; set; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
