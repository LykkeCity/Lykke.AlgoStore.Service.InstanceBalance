namespace Lykke.AlgoStore.Service.InstanceBalance.Domain
{
    public class WalletBalance
    {
        public string AssetId { get; set; }
        public decimal Balance { get; set; }
        public decimal Reserved { get; set; }
    }
}
