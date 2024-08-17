using StackExchange.Redis;

namespace BackgroungCacheInvalidation_project.Services;

public class Producer
{
    private readonly ISubscriber _subscriber;
    private readonly ILogger<Producer> _logger;
    private const string Channel = "messages";
    
    public Producer(IConnectionMultiplexer connectionMultiplexer, ILogger<Producer> logger)
    {
        _subscriber = connectionMultiplexer.GetSubscriber();
        _logger = logger;
    }

    protected async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.PublishAsync(Channel, "Produce!");

        _logger.LogInformation(
            $"Sending message to {Channel}");
    }
}