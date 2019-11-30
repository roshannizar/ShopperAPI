using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ShopperCart.MultiTenancy.Dto;

namespace ShopperCart.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

