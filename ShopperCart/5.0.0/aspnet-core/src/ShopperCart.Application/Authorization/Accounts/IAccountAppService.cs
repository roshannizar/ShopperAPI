using System.Threading.Tasks;
using Abp.Application.Services;
using ShopperCart.Authorization.Accounts.Dto;

namespace ShopperCart.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
