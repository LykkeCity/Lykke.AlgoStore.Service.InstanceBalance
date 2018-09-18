using JetBrains.Annotations;
using Lykke.AlgoStore.Security.InstanceAuth;

namespace Lykke.AlgoStore.Service.InstanceBalance.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AlgoInstanceBalanceSettings
    {
        public DbSettings Db { get; set; }
        public InstanceAuthSettings AuthSettings { get; set; }
    }
}
