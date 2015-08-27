using System;
using System.Collections.Generic;

namespace EfIdentities
{
    public class Order
    {
        public Order()
        {
            Lines = new List<OrderLine>();
        }

        public virtual int Id { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public string Product { get; set; }
    }
}
