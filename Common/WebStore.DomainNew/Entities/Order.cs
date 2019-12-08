using System;
using System.Collections;
using System.Collections.Generic;
using WebStore.DomainNew.Entities.Base;

namespace WebStore.DomainNew.Entities
{
    public class Order : NamedEntity
    {
        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime Date { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
