namespace Lykke.AlgoStore.Service.InstanceBalance.Client.Models
{
    /// <summary>
    /// Wallet balance for a given asset
    /// </summary>
    public class WalletBalanceModel
    {
        /// <summary>
        /// The asset this balance is for
        /// </summary>
        public string AssetId { get; set; }
        /// <summary>
        /// The balance for this asset
        /// </summary>
        public decimal Balance { get; set; }
        public decimal Reserved { get; set; }
    }
}
