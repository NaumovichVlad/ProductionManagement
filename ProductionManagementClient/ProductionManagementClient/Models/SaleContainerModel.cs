using System;

namespace ProductionManagementClient.Models
{
    public class SaleContainerModel : ModelBase
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CounteragentName { get; set; }
        public string Products { get; set; }
    }
}
