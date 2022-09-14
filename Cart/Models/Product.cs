using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cart.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public int ProductCost { get; set; }
        [Display(Name ="Description")]
        public string ProductDescrition { get; set; }
        [Required]
        [Display(Name ="Image")]
        public string ProductImage { get; set; }
    }
}