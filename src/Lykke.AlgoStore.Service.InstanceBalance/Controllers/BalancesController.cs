using Lykke.AlgoStore.Security.InstanceAuth;
using Lykke.AlgoStore.Service.InstanceBalance.Domain;
using Lykke.AlgoStore.Service.InstanceBalance.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.AlgoStore.Service.InstanceBalance.Controllers
{
    [Authorize]
    public class BalancesController : Controller
    {
        private readonly IWalletBalanceService _walletBalanceService;

        public BalancesController(IWalletBalanceService walletBalanceService)
        {
            _walletBalanceService = walletBalanceService ?? 
                throw new ArgumentNullException(nameof(walletBalanceService));
        }

        [HttpGet("api/v1/balances")]
        [SwaggerOperation(nameof(GetBalances))]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WalletBalance>))]
        public async Task<IActionResult> GetBalances()
        {
            var instanceData = User.GetInstanceData();

            var balances = await _walletBalanceService.GetWalletBalances(instanceData);

            return Ok(balances);
        }
    }
}
