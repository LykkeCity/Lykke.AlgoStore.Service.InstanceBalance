using Autofac;
using Lykke.AlgoStore.CSharp.AlgoTemplate.Models;
using Lykke.AlgoStore.Service.InstanceBalance.Extensions;
using Lykke.AlgoStore.Service.InstanceBalance.Settings;
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
        }
    }
}
