using Lykke.AlgoStore.CSharp.AlgoTemplate.Models.Models;
using Lykke.AlgoStore.Service.InstanceBalance.Domain;
using Lykke.AlgoStore.Service.InstanceBalance.Domain.Services;
using Lykke.AlgoStore.Service.InstanceBalance.DomainServices.Strings;
using Lykke.Service.Balances.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lykke.AlgoStore.Service.InstanceBalance.DomainServices
{
    public class WalletBalanceService : IWalletBalanceService
    {
        private readonly IBalancesClient _balancesClient;

        public WalletBalanceService(IBalancesClient balancesClient)
        {
            _balancesClient = balancesClient ?? throw new ArgumentNullException(nameof(balancesClient));
        }

        public async Task<IEnumerable<WalletBalance>> GetWalletBalances(AlgoClientInstanceData algoInstance)
        {
            if (algoInstance.AlgoInstanceType != CSharp.AlgoTemplate.Models.Enumerators.AlgoInstanceType.Live)
                throw new ValidationException(Phrases.InstanceMustBeLive);

            if (string.IsNullOrEmpty(algoInstance.WalletId))
                throw new ValidationException(Phrases.WalletIdEmpty);

            var balances = await _balancesClient.GetClientBalances(algoInstance.WalletId);

            return balances.Select(AutoMapper.Mapper.Map<WalletBalance>);
        }
    }
}
