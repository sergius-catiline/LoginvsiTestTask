namespace LoginvsiTestTask.Services;

/// <summary>
/// Represents a background service that reassigns tasks at intervals.
/// </summary>
public class TimerTaskAssignmentService(
    ILogger<TimerTaskAssignmentService> logger,
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    private readonly ILogger _logger = logger;
    private const int DelaySeconds = 120;
    private DateTime _lastRunTime = DateTime.MinValue;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    { 
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), stoppingToken);
            try
            {
                DoWork();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }

    private void DoWork()
    {
        using var scope = scopeFactory.CreateScope();
        var assignUserService = scope.ServiceProvider.GetRequiredService<IAssignUserService>();
        _logger.LogInformation($"Timer triggered at {DateTime.Now}");
        assignUserService.ReassignUsers(_lastRunTime);
        _lastRunTime = DateTime.Now;
        _logger.LogInformation($"Work is done");
    }
    
    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Background Service is stopping.");
        return Task.CompletedTask;
    }
}