using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Extensions.Options;
using UBike.Common.Settings;
using UBike.Hangfire.Infrastructure.HangfireMisc;
using UBike.Hangfire.Infrastructure.ServiceCollectionExtensions;
using UBike.Respository.Helpers;
using UBike.Respository.Implement;
using UBike.Respository.Interface;
using UBike.Service.Implement;
using UBike.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpClientFactory
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IYouBikeHttpClient, YouBikeHttpClient>();

// DatabaseConnectionOptions
builder.Services.AddDatabaseConnectionOptions();

// YouBikeDataSourceOptions
builder.Services.AddYouBikeDataSourceOptions();

// Mapster
builder.Services.AddMapster();

// Service - DI 註冊
builder.Services.AddScoped<IYoubikeOpenDataService, YoubikeOpenDataService>();
builder.Services.AddScoped<IYoubikeService, YoubikeService>();

// Repository - DI 註冊
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddScoped<IYoubikeRepository, YoubikeRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();

// HangfireMisc - DI 註冊
builder.Services.AddScoped<IHangfireJob, HangfireJob>();
builder.Services.AddScoped<IHangfireJobTrigger, HangfireJobTrigger>();

// Hangfire 的 SQL 連線字串設定
builder.Services.AddHangfire((serviceProvider, config) =>
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
builder.Services.AddHangfireServer(options =>
{
    // 機器名稱:專案名稱
    options.ServerName = $"{Environment.MachineName}:Ubike";

    // 預設是 20
    options.WorkerCount = 10;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/hangfire");
        return;
    }
    await next();
});

// HangFire Dashboard
app.UseHangfireDashboard
(
    "/hangfire",
    new DashboardOptions
    {
        Authorization = new List<IDashboardAuthorizationFilter>(),
        IgnoreAntiforgeryToken = true
    }
);

// Hangfire 系統啟動後的工作觸發點
using (var serviceScope = app.Services.CreateScope())
{
    // DI 註冊的提供者
    var serviceProvider = serviceScope.ServiceProvider;

    // 取得 IHangfireTrigger 的實體，透過替換 Interface 來選擇要哪個實體
    var jobTrigger = serviceProvider.GetRequiredService<IHangfireJobTrigger>();

    // 執行 HangfireTrigger 的 OnStart 動作，執行設定的排程
    jobTrigger.OnStart();
}

app.Run();