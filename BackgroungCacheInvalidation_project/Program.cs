using BackgroungCacheInvalidation_project.Services;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionMultiplexer>(await ConnectionMultiplexer.ConnectAsync("localhost:6379"));
builder.Services.AddScoped<Producer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

const string Channel = "messages";

app.MapGet("/", (IConnectionMultiplexer connectionMultiplexer) =>
    {
        connectionMultiplexer.GetSubscriber().Publish(Channel, "hello");
        return new OkResult();
    })
    .WithOpenApi();

app.Run();