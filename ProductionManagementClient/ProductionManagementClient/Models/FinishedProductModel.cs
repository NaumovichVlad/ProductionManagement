using System;

namespace ProductionManagementClient.Models
{
    public class FinishedProductModel : ModelBase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public bool IsApproved { get; set; }
        public string ProductName { get; set; }

        public override string ToString()
        {
            return ProductName;
        }
    }
}
