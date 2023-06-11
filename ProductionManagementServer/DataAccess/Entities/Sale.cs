using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class Sale : IEntity
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int CounteragentId { get; set; }
        public Counteragent Counteragent { get; set; }

    }
}
