namespace BackgroundWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }
    
    //The worker job
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //work until job is not canceled
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    //This is called when the worker starts up
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting...");
        return base.StartAsync(cancellationToken);
    }

    //This is called when the worker shuts down
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping...");
        return base.StopAsync(cancellationToken);
    }
}
