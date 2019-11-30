using System.Collections.Generic;
using ShopperCart.Roles.Dto;

namespace ShopperCart.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}