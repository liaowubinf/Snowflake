using System;
using Microsoft.Extensions.DependencyInjection;

namespace Essensoft.AspNetCore.Snowflake
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowflake(
            this IServiceCollection services)
        {
            services.AddSnowflake(null);
        }

        public static void AddSnowflake(
            this IServiceCollection services,
            Action<SnowflakeOptions> setupAction)
        {
            services.AddSingleton<IIdWorker, IdWorker>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
        }
    }
}