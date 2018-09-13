using Lykke.SettingsReader.Attributes;

namespace Lykke.AlgoStore.Service.InstanceBalance.Client 
{
    /// <summary>
    /// InstanceBalance client settings.
    /// </summary>
    public class InstanceBalanceServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
