using JetBrains.Annotations;
using Lykke.AlgoStore.Security.InstanceAuth;
using Lykke.AlgoStore.Service.InstanceBalance.Filters;
using Lykke.Common.Api.Contract.Responses;
using Lykke.Sdk;
using Lykke.AlgoStore.Service.InstanceBalance.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lykke.AlgoStore.CSharp.AlgoTemplate.Models.Mapper;

namespace Lykke.AlgoStore.Service.InstanceBalance
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly LykkeSwaggerOptions _swaggerOptions = new LykkeSwaggerOptions
        {
            ApiTitle = "InstanceBalance API",
            ApiVersion = "v1"
        };

        public Startup()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperModelProfile>();
            });
        }

        [UsedImplicitly]
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services.BuildServiceProvider<AppSettings>(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.Logs = logs =>
                {
                    logs.AzureTableName = "InstanceBalanceLog";
                    logs.AzureTableConnectionStringResolver = 
                    settings => settings.AlgoStoreInstanceBalanceService.Db.LogsConnString;

                    // TODO: You could add extended logging configuration here:
                    /* 
                    logs.Extended = extendedLogs =>
                    {
                        // For example, you could add additional slack channel like this:
                        extendedLogs.AddAdditionalSlackChannel("InstanceBalance", channelOptions =>
                        {
                            channelOptions.MinLogLevel = LogLevel.Information;
                        });
                    };
                    */
                };

                options.Extend = (sc, settings) =>
                {
                    sc.AddInstanceAuthentication(settings.CurrentValue.AlgoStoreInstanceBalanceService.AuthSettings);
                };

                options.Swagger = swagger =>
                {
                    swagger.OperationFilter<ApiKeyHeaderOperationFilter>();
                };
            });
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app)
        {
            app.UseLykkeConfiguration(options =>
            {
                options.SwaggerOptions = _swaggerOptions;

                options.WithMiddleware = x =>
                {
                    x.UseAuthentication();
                    x.Use(async (ctx, next) =>
                    {
                        try
                        {
                            await next();
                        }
                        catch(ValidationException ve)
                        {
                            ctx.Response.StatusCode = 400;
                            var err = JsonConvert.SerializeObject(ErrorResponse.Create(ve.Message));
                            await ctx.Response.WriteAsync(err);
                        }
                    });
                };
            });
        }
    }
}
