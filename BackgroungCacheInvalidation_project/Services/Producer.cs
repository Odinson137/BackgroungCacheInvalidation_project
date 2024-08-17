using StackExchange.Redis;

namespace BackgroungCacheInvalidation_project.Services;

public class Producer : BackgroundService
{
    private readonly ISubscriber _subscriber;
    private readonly ILogger<Consumer> _logger;
    private const string Channel = "messages";
    
    public Producer(IConnectionMultiplexer connectionMultiplexer, ILogger<Consumer> logger)
    {
        _subscriber = connectionMultiplexer.GetSubscriber();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _subscriber.PublishAsync(Channel, "Produce!");

            _logger.LogInformation(
                $"Sending message to {Channel}");

            await Task.Delay(5000, stoppingToken);
        }
    }
}