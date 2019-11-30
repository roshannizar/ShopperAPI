using Abp.Application.Services.Dto;
using ShopperCart.Customers.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopperCart.Order.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public virtual List<OrderLineDto> OrderItems { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual CustomerDto Customers { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public StatusTypeDto Status { get; set; }

        public OrderDto()
        {

        }

        public OrderDto(int customerId, DateTime dateTime, List<OrderLineDto> orderLineDtos, StatusTypeDto status)
        {
            this.CustomerId = customerId;
            this.Date = dateTime;
            this.OrderItems = orderLineDtos;
            this.Status = status;
        }
    }
}
