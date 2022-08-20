using BackgroundWorker;
using Coravel;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        //Here we configure scheduler
        services.AddScheduler();
        services.AddTransient<ProcessOrder>();
    })
    .Build();

//Configure Coravel scheduler
host.Services.UseScheduler(schedule =>
{
    //We define the jobSchedule for a certain ProcessOrder
    var jobSchedule = schedule.Schedule<ProcessOrder>();
    //Here we make a schedule: every weekday (so not Saturday and Sunday) at 9 in the morning.
    //jobSchedule.DailyAtHour(9).Weekday();
    //Here is every two seconds
    jobSchedule.EverySeconds(2);
});

await host.RunAsync();
