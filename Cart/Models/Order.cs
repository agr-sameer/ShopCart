using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}