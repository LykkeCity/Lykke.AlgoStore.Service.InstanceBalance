using Autofac;
using Lykke.AlgoStore.CSharp.AlgoTemplate.Models;
using Lykke.AlgoStore.Service.InstanceBalance.Domain.Services;
using Lykke.AlgoStore.Service.InstanceBalance.DomainServices;
using Lykke.AlgoStore.Service.InstanceBalance.Extensions;
using Lykke.AlgoStore.Service.InstanceBalance.Settings;
using Lykke.Service.Balances.Client;
using Lykke.SettingsReader;

namespace Lykke.AlgoStore.Service.InstanceBalance.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var dbConnString = _appSettings
                .Nested(x => x.AlgoStoreInstanceBalanceService.Db.DataStorageConnString);

            builder.RegisterRepository(log =>
                AzureRepoFactories.CreateAlgoClientInstanceRepository(dbConnString, log.CreateLog(this)));

            builder.RegisterType<WalletBalanceService>()
                .As<IWalletBalanceService>();

            builder.RegisterBalancesClient(_appSettings.CurrentValue.BalancesServiceClient);
        }
    }
}
