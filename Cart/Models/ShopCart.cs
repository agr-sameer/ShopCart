using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class ShopCart
    {
        public int CartId { get; set; }
        public int ProductQty { get; set; }
        public string ProductName { get; set; }
        public int ProductCost { get; set; }
        public int CartTotal { get; set; }
        public string ProductImage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}