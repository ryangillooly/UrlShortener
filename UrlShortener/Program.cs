using StackExchange.Redis;
using UrlShortener.Repositories;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"))
    .AddSingleton<IDatabaseRepository, RedisRepository>()
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