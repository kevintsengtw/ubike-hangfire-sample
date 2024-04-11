using Hangfire;
using Hangfire.Storage;

namespace UBike.Hangfire.Infrastructure.HangfireMisc;

/// <summary>
/// class HangfireJobTrigger
/// </summary>
/// <seealso cref="IHangfireJobTrigger" />
public class HangfireJobTrigger : IHangfireJobTrigger
{
    /// <summary>
    /// Called when [start].
    /// </summary>
    public void OnStart()
    {
        // 清除已有的排程工作
        using (var connection = JobStorage.Current.GetConnection())
        {
            foreach (var recurringJob in connection.GetRecurringJobs())
            {
                RecurringJob.RemoveIfExists(recurringJob.Id);
            }
        }

        // 定時執行任務 (Recurring)
        // 週期重複的工作，每 5 分鐘抓取 Youbike 公開資料，並更新到資料庫裡
        RecurringJob.AddOrUpdate<IYoubikeJob>(
            recurringJobId: "HangfireJob.UpdateYoubikeDataAsync",
            methodCall: s => s.UpdateYoubikeDataAsync(),
            cronExpression: "0/5 * * * *",
            options: new RecurringJobOptions { TimeZone = TimeZoneInfo.Local });
        
        // Cron Expression
        // https://crontab.guru
    }
}