using Lykke.HttpClientGenerator;

namespace Lykke.AlgoStore.Service.InstanceBalance.Client
{
    /// <summary>
    /// InstanceBalance API aggregating interface.
    /// </summary>
    public class InstanceBalanceClient : IInstanceBalanceClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to InstanceBalance Api.</summary>
        public IInstanceBalanceApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public InstanceBalanceClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IInstanceBalanceApi>();
        }
    }
}
