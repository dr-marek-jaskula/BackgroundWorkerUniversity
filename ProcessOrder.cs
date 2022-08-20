using Coravel.Invocable;

namespace BackgroundWorker;

//We implement the IInvocable interface from the Coravel.Invocable library
public class ProcessOrder : IInvocable
{
    private readonly ILogger<ProcessOrder> _logger;

    public ProcessOrder(ILogger<ProcessOrder> logger)
    {
        _logger = logger;
    }

    public async Task Invoke()
    {
        var jobId = Guid.NewGuid();
        _logger.LogInformation($"Starting job Id {jobId}");

        await Task.Delay(3000);
        _logger.LogInformation($"Ending job Id {jobId}");
    }
}
