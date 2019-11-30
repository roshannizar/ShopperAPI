using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ShopperCart.Roles.Dto;
using ShopperCart.Users.Dto;

namespace ShopperCart.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
