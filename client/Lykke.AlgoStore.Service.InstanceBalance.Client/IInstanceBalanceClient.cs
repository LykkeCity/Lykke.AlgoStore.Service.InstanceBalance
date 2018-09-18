using JetBrains.Annotations;

namespace Lykke.AlgoStore.Service.InstanceBalance.Client
{
    /// <summary>
    /// InstanceBalance client interface.
    /// </summary>
    [PublicAPI]
    public interface IInstanceBalanceClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IInstanceBalanceApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        IInstanceBalanceApi Api { get; }
    }
}
