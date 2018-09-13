using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.AlgoStore.Service.InstanceBalance.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public AlgoInstanceBalanceSettings AlgoStoreInstanceBalanceService { get; set; }
    }
}
