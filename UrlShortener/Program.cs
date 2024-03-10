using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime.CredentialManagement;
using StackExchange.Redis;
using UrlShortener.Repositories;
using UrlShortener.Services;
using UrlShortener.Settings;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("DynamoDbSettings");

var chain = new CredentialProfileStoreChain();
chain.TryGetAWSCredentials("Ryan", out var credentials);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .Configure<DynamoDbSettings>(settings)
    .AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2))
    .AddSingleton<DynamoDbSettings>()
    .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"))
   // .AddSingleton<IDatabaseRepository, RedisRepository>()
    .AddSingleton<IDatabaseRepository, DynamoDbRepository>()
    .AddSingleton<IDatabaseService,  DatabaseService>()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();