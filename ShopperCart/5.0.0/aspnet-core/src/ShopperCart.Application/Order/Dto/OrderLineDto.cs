using Abp.Application.Services.Dto;
using ShopperCart.Product.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopperCart.Order.Dto
{
    public class OrderLineDto:EntityDto<int>
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductDto Products { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual OrderDto Orders { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public bool IsDelete { get; set; }
    }
}
