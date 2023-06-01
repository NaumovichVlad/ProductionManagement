using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class MaterialPurchaseDto
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int PurchaseId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsAccepted { get; set; }
        public MaterialDto Material { get; set; }
        public PurchaseDto Purchase { get; set; }
    }
}
