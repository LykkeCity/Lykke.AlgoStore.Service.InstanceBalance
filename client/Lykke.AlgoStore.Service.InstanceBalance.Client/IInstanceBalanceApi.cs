using JetBrains.Annotations;
using Lykke.AlgoStore.Service.InstanceBalance.Client.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.AlgoStore.Service.InstanceBalance.Client
{
    // This is an example of service controller interfaces.
    // Actual interface methods must be placed here (not in IInstanceBalanceClient interface).

    /// <summary>
    /// InstanceBalance client API interface.
    /// </summary>
    [PublicAPI]
    public interface IInstanceBalanceApi
    {
        [Get("/api/v1/balances")]
        Task<IEnumerable<WalletBalanceModel>> GetBalancesAsync([Header("Authorization")] string authToken);
    }
}
