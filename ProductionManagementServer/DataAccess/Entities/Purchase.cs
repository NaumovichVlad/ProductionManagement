using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Purchase : IEntity
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ManufactureCountry { get; set; }
        public int CounteragentId { get; set; }
        public Counteragent Counteragent { get; set; }
    }
}
