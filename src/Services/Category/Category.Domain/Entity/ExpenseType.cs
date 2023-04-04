﻿using Core.Service.Domain;

namespace Category.Domain.Entity
{
    public class ExpenseType : ICreatedDate, IModifiedDate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
