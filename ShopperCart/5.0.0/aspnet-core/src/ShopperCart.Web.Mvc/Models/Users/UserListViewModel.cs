using System.Collections.Generic;
using ShopperCart.Roles.Dto;
using ShopperCart.Users.Dto;

namespace ShopperCart.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
