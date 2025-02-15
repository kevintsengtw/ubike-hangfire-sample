using Hangfire;
using Hangfire.Dashboard;
using Polly;
using Polly.Extensions.Http;
using UBike.Hangfire.Infrastructure.HangfireMisc;
using UBike.Hangfire.Infrastructure.ServiceCollectionExtensions;
using UBike.Repository.Helpers;
using UBike.Repository.Implement;
using UBike.Repository.Interface;
using UBike.Service.Implement;
using UBike.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpRequestException, 5XX (server errors) and 408 (RequestTimeout) are considered transient network failures
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                                      .WaitAndRetryAsync(new[]
                                      {
                                          TimeSpan.FromSeconds(1),
                                          TimeSpan.FromSeconds(5),
                                          TimeSpan.FromSeconds(10)
                                      });

// HttpClientFactory
builder.Services.AddHttpClient<IYouBikeHttpClient, YouBikeHttpClient>()
       .AddPolicyHandler(retryPolicy);

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

// Hangfire Feature
builder.Services.AddHangfireFeature()
       .AddRecurringJob<YoubikeRecurringJob>();

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

// 系統啟動後加入 Hangfire 排程
app.AddRecurringJobs();

app.Run();