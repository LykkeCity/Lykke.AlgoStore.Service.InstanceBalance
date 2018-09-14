using Autofac;
using Lykke.Common.Log;
using System;

namespace Lykke.AlgoStore.Service.InstanceBalance.Extensions
{
    internal static class ContainerBuilderExtensions
    {
        public static void RegisterRepository<T>(this ContainerBuilder builder, Func<ILogFactory, T> repo)
        {
            builder.Register(x =>
            {
                var log = x.Resolve<ILogFactory>();

                return repo(log);
            }).AsImplementedInterfaces();
        }
    }
}
