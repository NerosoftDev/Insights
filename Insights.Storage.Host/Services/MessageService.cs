namespace Nerosoft.Insights.Storage;

public class MessageService : BackgroundService
{
    private readonly MessageBus _bus;

    public MessageService(MessageBus bus)
    {
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.Run();
        await Task.CompletedTask;
    }
}