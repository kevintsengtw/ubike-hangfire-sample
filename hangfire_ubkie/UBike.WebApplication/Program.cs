using UBike.Respository.Helpers;
using UBike.Respository.Implement;
using UBike.Respository.Interface;
using UBike.Service.Implement;
using UBike.Service.Interface;
using UBike.WebApplication.Infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var basePath = AppContext.BaseDirectory;
    var xmlFiles = Directory.EnumerateFiles(basePath, searchPattern: "*.xml", SearchOption.TopDirectoryOnly);

    foreach (var xmlFile in xmlFiles)
    {
        options.IncludeXmlComments(xmlFile);
    }
});

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers()
   .WithOpenApi();

app.Run();