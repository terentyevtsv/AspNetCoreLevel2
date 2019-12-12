using System;
using System.Collections.Generic;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Dto
{
    public class OrderDto : NamedEntity
    {
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItemDto> OrderItems { get; set; }
    }
}
