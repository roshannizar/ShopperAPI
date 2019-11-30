using System.Threading.Tasks;
using ShopperCart.Configuration.Dto;

namespace ShopperCart.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
