using System;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Options;
using UBike.Common.Settings;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class HangfireFeatureServiceCollectionExtensions
/// </summary>
public static class HangfireFeatureServiceCollectionExtensions
{
    /// <summary>
    /// Adds the hangfire feature.
    /// </summary>
    /// <param name="services">The services</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddHangfireFeature(this IServiceCollection services)
    {
        // HangfireJob, HangfireJobTrigger
        services.AddScoped<IHangfireJob, HangfireJob>();
        services.AddScoped<IHangfireJobTrigger, HangfireJobTrigger>();

        // Hangfire 的 SQL 連線字串設定
        services.AddHangfire((serviceProvider, config) =>
        {
            // 1. 透過 serviceProvider 取得 DatabaseConnectionOptions
            // 2. 再從 DatabaseConnectionOptions 拿到 hangfire 資料庫連線字串

            var databaseConnectionOptions = serviceProvider.GetRequiredService<IOptions<DatabaseConnectionOptions>>();
            var databaseConnection = databaseConnectionOptions.Value;

            config.UseSqlServerStorage
            (
                nameOrConnectionString: databaseConnection.HangfireConnectionString,
                options: new SqlServerStorageOptions
                {
                    SchemaName = "UBike_Hangfire",
                    JobExpirationCheckInterval = TimeSpan.FromMinutes(60) // 設定 HangFireServer 每 60 分鐘清空過期的任務(保存超過1天)
                }
            );
        });

        // Hangfire Server 設定
        services.AddHangfireServer(options =>
        {
            // 機器名稱:專案名稱
            options.ServerName = $"{Environment.MachineName}:Ubike";

            // 預設是 20
            options.WorkerCount = 10;
        });
        
        return services;
    }
}