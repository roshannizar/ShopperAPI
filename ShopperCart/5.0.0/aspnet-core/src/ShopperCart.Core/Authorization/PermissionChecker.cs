using Abp.Authorization;
using ShopperCart.Authorization.Roles;
using ShopperCart.Authorization.Users;

namespace ShopperCart.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
