using System.Threading.Tasks;
using Abp.Application.Services;
using ShopperCart.Sessions.Dto;

namespace ShopperCart.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
