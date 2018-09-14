using AutoMapper;
using Lykke.AlgoStore.CSharp.AlgoTemplate.Models.Mapper;
using Lykke.AlgoStore.CSharp.AlgoTemplate.Models.Models;
using Lykke.AlgoStore.Service.InstanceBalance.DomainServices;
using Lykke.AlgoStore.Service.InstanceBalance.DomainServices.Strings;
using Lykke.Service.Balances.AutorestClient.Models;
using Lykke.Service.Balances.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Lykke.AlgoStore.Service.InstanceBalance.Tests
{
    public class WalletBalanceServiceTests
    {
        public WalletBalanceServiceTests()
        {
            Mapper.Reset();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperModelProfile>();
                cfg.AddProfile<ServicesMapperProfile>();
            });
        }

        [Fact]
        public void WalletBalanceService_ThrowsArgumentNull_WhenBalanceClientNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WalletBalanceService(null));
        }

        [Fact]
        public void WalletBalanceService_ThrowsValidationException_WhenInstanceNotLive()
        {
            var walletBalanceService = new WalletBalanceService(Mock.Of<IBalancesClient>());
            var instanceData = new AlgoClientInstanceData
            {
                AlgoInstanceType = CSharp.AlgoTemplate.Models.Enumerators.AlgoInstanceType.Demo
            };

            var ex = Assert.ThrowsAsync<ValidationException>(
                () => walletBalanceService.GetWalletBalances(instanceData)).Result;

            Assert.Equal(Phrases.InstanceMustBeLive, ex.Message);
        }

        [Fact]
        public void WalletBalanceService_ThrowsValidationException_WhenInstanceWalletEmpty()
        {
            var walletBalanceService = new WalletBalanceService(Mock.Of<IBalancesClient>());
            var instanceData = new AlgoClientInstanceData
            {
                AlgoInstanceType = CSharp.AlgoTemplate.Models.Enumerators.AlgoInstanceType.Live
            };

            var ex = Assert.ThrowsAsync<ValidationException>(
                () => walletBalanceService.GetWalletBalances(instanceData)).Result;

            Assert.Equal(Phrases.WalletIdEmpty, ex.Message);
        }

        [Fact]
        public void WalletBalanceService_ReturnsCorrectData_WhenEverythingValid()
        {
            var balancesClient = new Mock<IBalancesClient>(MockBehavior.Strict);
            balancesClient.Setup(x => x.GetClientBalances(It.IsAny<string>()))
                .ReturnsAsync(new List<ClientBalanceResponseModel>
                {
                    new ClientBalanceResponseModel
                    {
                        AssetId = "a",
                        Balance = 1,
                        Reserved = 2
                    }
                });
            var instanceData = new AlgoClientInstanceData
            {
                AlgoInstanceType = CSharp.AlgoTemplate.Models.Enumerators.AlgoInstanceType.Live,
                WalletId = "asdf"
            };

            var walletBalanceService = new WalletBalanceService(balancesClient.Object);

            var result = walletBalanceService.GetWalletBalances(instanceData).Result.First();

            Assert.Equal("a", result.AssetId);
            Assert.Equal(1, result.Balance);
            Assert.Equal(2, result.Reserved);
        }
    }
}
