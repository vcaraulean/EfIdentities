using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfIdentities
{
    public class Order
    {
        public Order()
        {
            Lines = new List<OrderLine>();
        }

        [Key]
        public virtual int Id { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<OrderLine> Lines { get; set; }
    }

    public class OrderLine
    {
        [Key, Required, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, ForeignKey("Order"), Column(Order = 1)]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string Product { get; set; }
    }
}
