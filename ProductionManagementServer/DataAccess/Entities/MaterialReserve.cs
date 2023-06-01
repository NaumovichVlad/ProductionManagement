using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class MaterialReserve : IEntity
    {
        public int Id { get; set; }
        public int StoragePlaceId { get; set; }
        public int MaterialPurchaseId { get; set; }
        public int Count { get; set; }
        public StoragePlace StoragePlace { get; set; }
        public MaterialPurchase MaterialPurchase { get; set; }
    }
}
