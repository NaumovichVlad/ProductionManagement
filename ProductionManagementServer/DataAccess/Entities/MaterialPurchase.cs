using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class MaterialPurchase : IEntity
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int PurchaseId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public bool IsAccepted { get; set; }
        public Material Material { get; set; }
        public Purchase Purchase { get; set; }
    }
}
