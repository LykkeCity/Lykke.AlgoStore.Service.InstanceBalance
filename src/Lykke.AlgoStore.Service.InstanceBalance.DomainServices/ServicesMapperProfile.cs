using AutoMapper;
using Lykke.AlgoStore.Service.InstanceBalance.Domain;
using Lykke.Service.Balances.AutorestClient.Models;

namespace Lykke.AlgoStore.Service.InstanceBalance.DomainServices
{
    public class ServicesMapperProfile : Profile
    {
        public ServicesMapperProfile()
        {
            CreateMap<ClientBalanceResponseModel, WalletBalance>();
        }
    }
}
