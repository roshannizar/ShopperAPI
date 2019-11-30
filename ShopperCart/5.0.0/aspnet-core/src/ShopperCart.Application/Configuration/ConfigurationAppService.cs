using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ShopperCart.Configuration.Dto;

namespace ShopperCart.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ShopperCartAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
