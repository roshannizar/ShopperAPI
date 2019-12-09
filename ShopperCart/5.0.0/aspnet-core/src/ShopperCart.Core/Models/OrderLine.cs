using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopperCart.Models
{
    public class OrderLine:Entity<int>
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public bool IsDeleted { get; set; }
    }
}
