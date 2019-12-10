using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopperCart.Customers.Dto
{
    public class CustomerDto:EntityDto<int>
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
