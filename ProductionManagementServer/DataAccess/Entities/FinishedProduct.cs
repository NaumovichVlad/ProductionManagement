﻿using DataAccess.Interfaces;

namespace DataAccess.Entities
{
    public class FinishedProduct : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ManufactureDate { get; set; }
        public Product Product { get; set; }

    }
}
