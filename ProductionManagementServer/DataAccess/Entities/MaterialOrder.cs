using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class MaterialOrder : IEntity
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public int CounteragentId { get; set; }
        public Counteragent Counteragent { get; set; }
        public Material Material { get; set; }
    }
}
