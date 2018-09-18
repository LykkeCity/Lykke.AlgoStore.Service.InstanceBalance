using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.Balances.Client;

namespace Lykke.AlgoStore.Service.InstanceBalance.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public AlgoInstanceBalanceSettings AlgoStoreInstanceBalanceService { get; set; }
        public BalancesServiceClientSettings BalancesServiceClient { get; set; }
    }
}
