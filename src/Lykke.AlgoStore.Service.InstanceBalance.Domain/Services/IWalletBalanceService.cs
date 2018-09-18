using Lykke.AlgoStore.CSharp.AlgoTemplate.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.AlgoStore.Service.InstanceBalance.Domain.Services
{
    public interface IWalletBalanceService
    {
        Task<IEnumerable<WalletBalance>> GetWalletBalances(AlgoClientInstanceData algoInstance);
    }
}
