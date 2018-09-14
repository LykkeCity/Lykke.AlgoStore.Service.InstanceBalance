using Lykke.SettingsReader.Attributes;

namespace Lykke.AlgoStore.Service.InstanceBalance.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        [AzureTableCheck]
        public string DataStorageConnString { get; set; }
    }
}
