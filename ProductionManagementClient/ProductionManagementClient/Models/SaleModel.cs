using System;

namespace ProductionManagementClient.Models
{
    public class SaleModel : ModelBase
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CounteragentId { get; set; }
        public string CounteragentName { get; set; }

        public override string ToString()
        {
            return OrderNumber.ToString();
        }
    }
}
