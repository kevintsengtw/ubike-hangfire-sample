namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class HangfireFeatureApplicationBuilderExtensions
/// </summary>
public static class HangfireFeatureApplicationBuilderExtensions
{
    /// <summary>
    /// Starts the recurring jobs.
    /// </summary>
    /// <param name="app">The app</param>
    /// <returns>The app</returns>
    public static IApplicationBuilder StartRecurringJobs(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        
        // DI 註冊的提供者
        var serviceProvider = serviceScope.ServiceProvider;

        // 取得 IHangfireTrigger 的實體，透過替換 Interface 來選擇要哪個實體
        var jobTrigger = serviceProvider.GetRequiredService<IHangfireJobTrigger>();

        // 執行 HangfireTrigger 的 OnStart 動作，執行設定的排程
        jobTrigger.OnStart();

        return app;
    }
}