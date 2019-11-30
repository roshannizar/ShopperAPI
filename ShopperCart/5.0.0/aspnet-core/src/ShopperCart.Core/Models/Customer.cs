using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopperCart.Models
{
    public class Customer:Entity<int>
    {
        [Required]
        [StringLength(50, ErrorMessage = "Reached the maximum limit")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Reached the maximum limit")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }
}
