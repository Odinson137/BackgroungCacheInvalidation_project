using StackExchange.Redis;

namespace Consumer2_project.Services;

public class Consumer : BackgroundService
{
    private readonly ISubscriber _subscriber;
    private readonly ILogger<Consumer> _logger;
    private const string Channel = "messages";
    
    public Consumer(IConnectionMultiplexer connectionMultiplexer, ILogger<Consumer> logger)
    {
        _subscriber = connectionMultiplexer.GetSubscriber();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync(Channel, (channel, message) =>
        {
            _logger.LogInformation($"Hello from consumer 2 with message! {channel}: {message}");
        });
    }
}