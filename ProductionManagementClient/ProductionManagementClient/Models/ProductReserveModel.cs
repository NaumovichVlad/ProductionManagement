using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionManagementClient.Models
{
    public class ProductReserveModel : ModelBase
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int FinishedProductId { get; set; }
        public int Count { get; set; }
        public string StoragePlaceName { get; set; }
        public string FinishedProductName { get; set; }

        public override string ToString()
        {
            return FinishedProductName;
        }
    }
}
